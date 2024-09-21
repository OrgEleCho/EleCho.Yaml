using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using EleCho.Yaml.Internals;
using EleCho.Yaml.Utilities;

namespace EleCho.Yaml.Parsing
{
    /// <summary>
    /// Yaml lexer, Yaml tokens are produced by the lexer. <br/>
    /// Yaml 此法分析器, Yaml 单词由词法分析器产生。
    /// </summary>
    internal ref struct YamlLexer
    {
        private readonly TextReader? _readerSource;
        private readonly ReadOnlyMemory<char> _memorySource;
        private readonly IndexableQueue<char> _characterQueue;
        private readonly CharacterBuffer _characterBuffer;

        private YamlToken _currentToken = YamlToken.None;
        private bool _lastCharIsNewLine = false;

        private int _index = 0;
        private int _lineNumber = 1;
        private int _positionInLine = 0;

        public YamlLexerOptions Options { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="YamlLexer"/> class. <br/>
        /// 初始化 <see cref="YamlLexer"/> 类的新实例。
        /// </summary>
        /// <param name="reader"><see cref="TextReader"/> to use.  <br/>要使用的 <see cref="TextReader"/></param>
        public YamlLexer(TextReader reader, YamlLexerOptions options)
        {
            _readerSource = reader;
            Options = options;
        }

        public YamlLexer(ReadOnlyMemory<char> text, YamlLexerOptions options)
        {
            _memorySource = text;
            Options = options;
        }

        private int CoreRead()
        {
            if (_readerSource is not null)
            {
                return _readerSource.Read();
            }
            else
            {
                if (_index < _memorySource.Length)
                {
                    return _memorySource.Span[_index];
                }
                else
                {
                    return -1;
                }
            }
        }

        private int Peek(int offset)
        {
            while (_characterQueue.Count < offset)
            {
                var result = CoreRead();
                if (result == -1)
                {
                    return -1;
                }

                _characterQueue.Enqueue((char)result);
            }

            return _characterQueue.Peek(offset);
        }

        private int Peek()
        {
            return Peek(0);
        }

        private int Read()
        {
            int result;
            if (_characterQueue.Count > 0)
            {
                return _characterQueue.Dequeue();
            }

            result = CoreRead();
            if (result is '\r' or '\n')
            {
                if (!_lastCharIsNewLine)
                {
                    _lineNumber++;
                    _positionInLine = 0;
                }

                _lastCharIsNewLine = true;
            }
            else
            {
                _positionInLine++;
                _lastCharIsNewLine = false;
            }

            return result;
        }

        private ReadOnlyMemory<char> Slice(int startIndex, int length)
        {
            if (_readerSource is not null)
            {
                return _characterBuffer.Slice(startIndex, length);
            }
            else
            {
                return _memorySource.Slice(startIndex, length);
            }
        }

        private ReadOnlyMemory<char> ReadChar()
        {
            int result = Read();
            if (result == -1)
            {
                throw new InvalidOperationException();
            }

            return _characterBuffer.Slice(_index - 1, 1);
        }

        private ReadOnlyMemory<char> ReadString()
        {
            var currentChar = default(int);
            var startIndex = _index;

            currentChar = Read();   // skip the first '"' 跳过第一个双引号

            bool escape = false;
            StringBuilder sb = new StringBuilder();
            while (currentChar != -1)
            {
                currentChar = Read();

                if (escape)
                {
                    escape = false;
                    sb.Append(currentChar switch
                    {
                        '0' => '\0',
                        'a' => '\a',
                        'b' => '\b',
                        't' => '\t',
                        'r' => '\r',
                        'f' => '\f',
                        'n' => '\n',
                        'v' => '\v',
                        _ => currentChar
                    });
                }
                else
                {
                    if (currentChar == '\\')
                    {
                        escape = true;
                    }
                    else if (currentChar == '"')
                    {
                        // " is already readed, so we need to read the next char, 双引号已经被读取了, 所以我们不需要读取下一个字符
                        return _characterBuffer(startIndex, )
                    }
                    else
                    {
                        sb.Append((char)currentChar);
                    }
                }
            }

            throw new InvalidOperationException("Unexpected end of stream");
        }

        private ReadOnlyMemory<char> ReadLiteral()
        {
            var tokenStartIndex = _index;

            while (true)
            {
                int cur = Peek();

                if (char.IsWhiteSpace((char)cur) ||
                    (cur >= '0' && cur <= '9') ||
                    (cur >= 'A' && cur <= 'Z') ||
                    (cur >= 'a' && cur <= 'z') ||
                    cur == '-')
                {
                    Read();
                }
                else
                {
                    return Slice(tokenStartIndex, _index - tokenStartIndex);
                }
            }
        }

        private ReadOnlyMemory<char> ReadComment()
        {
            var tokenStartIndex = _index;
            var commentStart = Read();
            var newLine = false;

            if (commentStart != '#')
            {
                throw new InvalidOperationException();
            }

            while (true)
            {
                var current = Peek();
                if (current is '\r' or '\n')
                {
                    newLine = true;
                }
                else if (!char.IsWhiteSpace((char)current))
                {
                    if (newLine)
                    {
                        break;
                    }
                }

                // consume the current character
                Read();
            }

            return Slice(tokenStartIndex, _index - tokenStartIndex);
        }

        private ReadOnlyMemory<char> ReadAnchor()
        {
            var tokenStartIndex = _index;
            var anchorStart = Read();    // skip the first '&'

            while (true)
            {
                int cur = Peek();

                if ((cur >= '0' && cur <= '9') ||
                    (cur >= 'A' && cur <= 'Z') ||
                    (cur >= 'a' && cur <= 'z') ||
                    cur == '-')
                {
                    Read();
                }
                else
                {
                    return Slice(tokenStartIndex, _index - tokenStartIndex);
                }
            }
        }

        private ReadOnlyMemory<char> ReadReference()
        {
            StringBuilder sb = new();
            Read();    // skip the first '*'

            while (true)
            {
                int cur = Peek();

                if ((cur >= '0' && cur <= '9') ||
                    (cur >= 'A' && cur <= 'Z') ||
                    (cur >= 'a' && cur <= 'z') ||
                    cur == '-')
                {
                    Read();
                    sb.Append((char)cur);
                }
                else
                {
                    return sb.ToString();
                }
            }
        }

        private void SkipWhiteSpace()
        {
            int cur;

            while (true)
            {
                cur = Peek();  // skip the whitespace. 跳过空白
                if (char.IsWhiteSpace((char)cur))
                {
                    Read();
                }
                else
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Reads the next token from the lexer. (If <see cref="PeekToken"/> was called before read, cache token will be first use) <br/>
        /// 从词法分析器读取下一个令牌。(如果 <see cref="PeekToken"/> 在读取之前被调用，缓存令牌将首先使用)
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public YamlToken NextToken()
        {
            if (!_currentToken.IsNone())
            {
                YamlToken rst = _currentToken;
                _currentToken = YamlToken.None;
                return rst;
            }

            int curChar = Peek();
            if (curChar == -1)
            {
                return YamlToken.None;
            }

            SkipWhiteSpace();
            curChar = Peek();

            var position = _positionInLine;
            return curChar switch
            {
                '{' => new YamlToken(YamlTokenKind.ObjectStart, ReadChar(), _lineNumber, position),
                '}' => new YamlToken(YamlTokenKind.ObjectEnd, ReadChar(), _lineNumber, position),
                '[' => new YamlToken(YamlTokenKind.ArrayStart, ReadChar(), _lineNumber, position),
                ']' => new YamlToken(YamlTokenKind.ArrayEnd, ReadChar(), _lineNumber, position),
                '#' => new YamlToken(YamlTokenKind.Comment, ReadComment(), _lineNumber, position),
                '"' => new YamlToken(YamlTokenKind.String, ReadString(), _lineNumber, position),
                ':' => new YamlToken(YamlTokenKind.Colon, ReadChar(), _lineNumber, position),
                ',' => new YamlToken(YamlTokenKind.Comma, ReadChar(), _lineNumber, position),
                '-' => new YamlToken(YamlTokenKind.Dash, ReadChar(), _lineNumber, position),
                '&' => new YamlToken(YamlTokenKind.Anchor, ReadAnchor(), _lineNumber, position),
                '*' => new YamlToken(YamlTokenKind.Reference, ReadReference(), _lineNumber, position),
                _ when char.IsLetter() => new YamlToken(YamlTokenKind.Literal, ReadLiteral(), _lineNumber, position),
                _ => throw new InvalidOperationException($"Unexpected char '{(char)curChar}'")
            };
        }

        private static bool IsLiteralStart(char c)
        {

        }

        private static bool IsLiteral(char c)
        {
            
        }

        public static IEnumerable<YamlToken> Lex(TextReader input, YamlLexerOptions options)
        {
            var lexer = new YamlLexer(input, options);

            while (lexer.PeekToken().IsNotNone())
            {
                yield return lexer.NextToken();
            }
        }

        public static IEnumerable<YamlToken> Lex(ReadOnlySpan<char> input, YamlLexerOptions options)
        {
            var lexer = new YamlLexer(input, options);

            while (lexer.PeekToken().IsNotNone())
            {
                yield return lexer.NextToken();
            }
        }
    }
}