using System;
using System.IO;
using EleCho.Yaml.Internals;

namespace EleCho.Yaml.Parsing
{
    internal ref struct YamlBufferedLexer
    {
        private readonly YamlLexer _coreLexer;
        private readonly IndexableQueue<YamlToken> _buffer;

        public YamlToken PeekToken(int offset)
        {
            while (_buffer.Count < offset)
            {
                _buffer.Enqueue(_coreLexer.NextToken());
            }

            return _buffer.Peek(offset);
        }

        public YamlToken PeekToken()
        {
            return PeekToken(0);
        }

        public YamlToken ReadToken()
        {
            if (_buffer.Count > 1)
            {
                return _buffer.Dequeue();
            }

            return _coreLexer.NextToken();
        }

        public void Dispose()
        {
            _buffer.Dispose();
        }

        public YamlBufferedLexer(TextReader reader, YamlLexerOptions options)
        {
            _coreLexer = new YamlLexer(reader, options);
        }

        public YamlBufferedLexer(ReadOnlySpan<char> text, YamlLexerOptions options)
        {
            _coreLexer = new YamlLexer(text, options);
        }
    }
}