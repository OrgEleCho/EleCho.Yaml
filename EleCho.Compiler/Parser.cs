using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace EleCho.Compiler
{
    public class Parser
    {
        int _currentIndex = 0;
        int _lineNumber = 0;
        int _positionInLine = 0;
        bool _isInNewLine = false;


        public IEnumerable<IGrammar> Grammars { get; }
        public ReadOnlyMemory<char> Input { get; }
        public ParsingResult Result { get; }

        public Parser(
            IEnumerable<IGrammar> grammars,
            ReadOnlyMemory<char> input)
        {
            Grammars = grammars;
            Input = input;
            Result = new();
        }

        public bool Do()
        {
            if (_currentIndex >= Input.Length)
            {
                return false;
            }

            var currentChar = Input.Span[_currentIndex];

            if (currentChar is '\r' or '\n')
            {
                if (!_isInNewLine)
                {
                    _lineNumber++;
                    _positionInLine = 0;
                }

                _isInNewLine = true;
            }
            else
            {
                _positionInLine++;
            }

            if (Result.GetTail(0) is CharSequence charSequence)
            {
                charSequence.TextEnd++;
            }
            else
            {
                Result.Add(new CharSequence(Input, _currentIndex, 1, _lineNumber, _positionInLine));
            }

            foreach (var grammar in Grammars)
            {
                if (grammar.RequireCount > Result.Count)
                {
                    continue;
                }

                var syntaxes = Result.GetTails(grammar.RequireCount);
                if (grammar.CanConstruct(syntaxes))
                {
                    var newSyntax = grammar.Construct(syntaxes);
                    Result.RemoveTails(grammar.RequireCount);
                }
            }

            _currentIndex++;
            return _currentIndex >= Input.Length;
        }
    }
}
