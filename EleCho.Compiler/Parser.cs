using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace EleCho.Compiling
{

    public class Parser
    {
        private int _currentIndex = 0;
        private int _lineNumber = 0;
        private int _positionInLine = 0;
        private bool _lastChanged = false;
        private bool _isInNewLine = false;
        private EndOfFile? _endOfFile = null;

        private readonly LinkedList<ISyntax> _newSyntaxesBuffer = new();


        public IEnumerable<IGrammar> Grammars { get; }
        public ReadOnlyMemory<char> Input { get; }
        public ParsingQueue Result { get; }

        public bool EndOfFile => Result.EndOfFile;

        public Parser(
            IEnumerable<IGrammar> grammars,
            ReadOnlyMemory<char> input)
        {
            Grammars = grammars;
            Input = input;
            Result = new();
        }

        private bool DoCore()
        {
            _lastChanged = false;

            foreach (var grammar in Grammars)
            {
                // 判断现有的 Syntax 数量是否满足 grammar 的需求
                if (grammar.RequireCount > Result.Count)
                {
                    continue;
                }

                // 取出来末尾的一些 syntax
                var context = new GrammarContext(this, grammar);
                var syntaxes = Result.GetTails(grammar.RequireCount);

                // 如果可以进行构造
                if (grammar.CanConstruct(context, syntaxes.Span))
                {
                    // 则构造
                    var newSyntaxes = grammar.Construct(context, syntaxes);

                    // 缓存新 Syntax
                    if (newSyntaxes is not null)
                    {
                        var lastNode = default(LinkedListNode<ISyntax>);
                        foreach (var newSyntax in newSyntaxes)
                        {
                            if (newSyntax is null)
                            {
                                continue;
                            }

                            if (lastNode is null)
                            {
                                lastNode = _newSyntaxesBuffer.AddFirst(newSyntax);
                            }
                            else
                            {
                                lastNode = _newSyntaxesBuffer.AddAfter(lastNode, newSyntax);
                            }
                        }
                    }

                    // 删除旧 Syntax
                    Result.RemoveTails(grammar.RequireCount);

                    while (_newSyntaxesBuffer.First is not null)
                    {
                        Result.Add(_newSyntaxesBuffer.First.Value);
                        _newSyntaxesBuffer.RemoveFirst();
                    }

                    // 标记有表更
                    _lastChanged = true;
                    break;
                }
            }

            return _lastChanged;
        }
        
        private bool DoWhileLastChanged()
        {
            DoCore();
            return true;
        }

        private bool DoWhileNeedNewCharacter()
        {
            var currentChar = Input.Span[_currentIndex];
            var nextLineNumber = _lineNumber;
            var nextPositionInLine = _positionInLine;

            if (currentChar is '\r' or '\n')
            {
                if (!_isInNewLine)
                {
                    nextLineNumber++;
                }

                _isInNewLine = true;
                nextPositionInLine = 0;
            }
            else
            {
                _isInNewLine = false;
                nextPositionInLine++;
            }

            if (Result.Count > 0 && Result.GetTail(0) is CharSequence charSequence)
            {
                charSequence.TextEnd++;
            }
            else
            {
                Result.Add(new CharSequence(Input, _currentIndex, _currentIndex + 1, _lineNumber, _positionInLine, nextPositionInLine));
            }

            DoCore();

            _currentIndex++;
            _lineNumber = nextLineNumber;
            _positionInLine = nextPositionInLine;

            return true;
        }

        private bool DoWhileEndOfFile()
        {
            if (DoCore())
            {
                return true;
            }

            if (Result.Count > 0 && Result.GetTail(0) is not EleCho.Compiling.EndOfFile)
            {
                var endOfFile = _endOfFile ??= new EndOfFile(Input, _currentIndex, _lineNumber, _positionInLine);
                Result.Add(endOfFile);
            }

            var changed = DoCore();

            if (changed && Result.Count > 0 && Result.GetTail(0) is EleCho.Compiling.EndOfFile)
            {
                Result.RemoveTail();
            }

            return changed;
        }

        public bool Do()
        {
            if (_lastChanged)
            {
                return DoWhileLastChanged();
            }
            else if (_currentIndex < Input.Length)
            {
                return DoWhileNeedNewCharacter();
            }
            else
            {
                return DoWhileEndOfFile();
            }
        }
    }
}
