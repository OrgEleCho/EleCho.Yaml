using System.Collections.Generic;
using System.IO;
using System;
using EleCho.Yaml.Nodes;
using EleCho.Yaml.Extensions;
using System.Diagnostics.CodeAnalysis;
using EleCho.Yaml.Internals;

namespace EleCho.Yaml.Parsing
{
    /// <summary>
    /// Represents 
    /// </summary>
    internal ref struct YamlParser
    {
        /// <summary>
        /// Base lexer
        /// </summary>
        private readonly YamlBufferedLexer _lexer;

        public YamlParserOptions Options { get; }

        /// <summary>
        /// Create a new instance of the <see cref="YamlParser"/> class.
        /// </summary>
        /// <param name="reader"></param>
        public YamlParser(TextReader reader, YamlParserOptions options)
        {
            _lexer = new YamlBufferedLexer(reader, new YamlLexerOptions()
            {
                AllowHorizontalTab = options.AllowHorizontalTab,
            });

            Options = options;
        }

        /// <summary>
        /// Create a new instance of the <see cref="YamlParser"/> class.
        /// </summary>
        /// <param name="yaml">Yaml text</param>
        public YamlParser(ReadOnlySpan<char> yaml, YamlParserOptions options)
        {
            _lexer = new YamlBufferedLexer(yaml, new YamlLexerOptions()
            {
                AllowHorizontalTab = options.AllowHorizontalTab,
            });

            Options = options;
        }

        public YamlParser(Stream stream, YamlParserOptions options) 
            : this(new StreamReader(stream), options)
        {

        }

        public YamlParser(string yaml, YamlParserOptions options)
            : this(yaml.AsSpan(), options)
        {

        }





        /// <summary>
        /// Create a new <see cref="YamlParser"/> and use it to read a <see cref="YamlNode"/> from the stream.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static YamlNode Read(Stream stream, YamlParserOptions options)
        {
            return new YamlParser(stream, options).Read();
        }

        /// <summary>
        /// Create a new <see cref="YamlParser"/> and use it to read a <see cref="YamlNode"/> from the TextReader.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static YamlNode Read(TextReader reader, YamlParserOptions options)
        {
            return new YamlParser(reader, options).Read();
        }

        /// <summary>
        /// Create a new <see cref="YamlParser"/> and use it to read a <see cref="YamlNode"/> from the string.
        /// </summary>
        /// <param name="yaml">Yaml text</param>
        /// <returns></returns>
        public static YamlNode Read(string yaml, YamlParserOptions options)
        {
            return new YamlParser(yaml, options).Read();
        }

        internal bool InternalReadYamlObjectEntry(ref int? currentIndent, [NotNullWhen(true)] out string? key, [NotNullWhen(true)] out YamlNode? value)
        {
            var keyToken = _lexer.PeekToken();
            if (keyToken.Kind != YamlTokenKind.Literal)
            {
                key = default;
                value = default;
                return false;
            }

            if (currentIndent.HasValue)
            {
                if (keyToken.Position < currentIndent)
                {
                    key = default;
                    value = default;
                    return false;
                }
                else if (keyToken.Position > currentIndent)
                {
                    throw new YamlException($"Invalid indent: {keyToken}");
                }
            }

            // read colon
            _lexer.ReadTokenAndEnsureKind(YamlTokenKind.Colon);

            // anchor or none
            var anchorToken = _lexer.ReadTokenOrDefault(YamlTokenKind.Anchor);

            // literal, string or none
            var valueToken = _lexer.ReadTokenOrDefault(token => token.Is(YamlTokenKind.Literal) || token.Is(YamlTokenKind.String));

            // has literal or string value
            if (!valueToken.IsNone())
            {
                key = keyToken.Text.ToString();
                value = new YamlValue(valueToken.Text, valueToken.Is(YamlTokenKind.String), null);
                if (anchorToken.Is(YamlTokenKind.Anchor))
                {
                    value.AnchorName = anchorToken.Text.ToString();
                }

                return true;
            }


        }

        internal bool InternalReadYamlArrayEntry(ref int? currentIndent, [NotNullWhen(true)] out YamlNode? value)
        {

        }

        internal YamlObject? InternalReadYamlObject()
        {
            int? currentIndent = null;
            var dataDict = default(Dictionary<string, YamlNode>);

            while (InternalReadYamlObjectEntry(ref currentIndent, out var key, out var value))
            {
                if (dataDict is null)
                {
                    dataDict = new();
                }

                dataDict[key] = value;
            }

            if (dataDict is null)
            {
                return null;
            }

            return new YamlObject(dataDict);
        }

        internal YamlArray? InternalReadYamlArray()
        {

        }

        /// <summary>
        /// take a object from stream (thw following token must be object start)
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        internal YamlObject? InternalReadJsonObject()
        {
            Dictionary<string, YamlNode> dataDict = new ();
            var objectStartToken = _lexer.PeekToken();
            if (!objectStartToken.Is(YamlTokenKind.ObjectStart))
            {
                return null;
            }

            // consume object start
            _lexer.ReadToken();

            while (true)
            {
                YamlToken keyToken = _lexer.PeekToken();   // move to key
                if (keyToken.Kind == YamlTokenKind.ObjectEnd)
                    break;
                if (keyToken.Kind != YamlTokenKind.String)
                    throw new InvalidOperationException("Unexpected token " + keyToken.Kind);

                string? key = _lexer.ReadToken().Text.ToString();
                YamlToken colonToken = _lexer.ReadToken();
                if (colonToken.Kind != YamlTokenKind.Colon)
                    throw new InvalidOperationException("Unexpected token " + keyToken.Kind);

                dataDict[key!] = InternalReadJson();

                YamlToken endOrCommaToken = _lexer.PeekToken();
                if (endOrCommaToken.Kind == YamlTokenKind.ObjectEnd)
                    break;
                if (endOrCommaToken.Kind == YamlTokenKind.Comma)
                    _lexer.ReadToken();   // skip comma
                else
                    throw new InvalidOperationException("Unexpected token " + endOrCommaToken.Kind);
            }

            _lexer.ReadToken();  // skip object end
            return new YamlObject(dataDict);
        }

        /// <summary>
        /// take a array from stream (thw following token must be array start)
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        internal YamlArray? InternalReadJsonArray()
        {
            bool hasLastComma = false;
            List<YamlNode> dataArr = new ();
            var arrayStartToken = _lexer.PeekToken();
            if (!arrayStartToken.Is(YamlTokenKind.ArrayStart))
            {
                return null;
            }

            // consume array start
            _lexer.ReadToken();

            while (true)
            {
                YamlToken valueToken = _lexer.PeekToken();   // move to value
                if (valueToken.Kind == YamlTokenKind.ArrayEnd)
                {
                    if (hasLastComma)
                        break;
                }

                dataArr.Add(InternalReadJson());

                YamlToken endOrCommaToken = _lexer.PeekToken();

                if (endOrCommaToken.Kind == YamlTokenKind.ArrayEnd)
                {
                    break;
                }
                else if (endOrCommaToken.Kind == YamlTokenKind.Comma)
                {
                    _lexer.ReadToken();    // skip comma
                    hasLastComma = true;
                }
                else
                {
                    throw new InvalidOperationException("Unexpected token " + endOrCommaToken.Kind);
                }
            }

            _lexer.ReadToken(); // skip array end
            return new YamlArray(dataArr);
        }

        internal YamlValue? InternalReadYamlValue()
        {
            var token = _lexer.PeekToken();
            if (token.Kind != YamlTokenKind.String &&
                token.Kind != YamlTokenKind.Literal)
            {
                return null;
            }

            return new YamlValue(token.Text, token.Kind == YamlTokenKind.String);
        }

        internal YamlNode? ReadYamlObjectArrayOrValue()
        {
            var value =
                (YamlNode?)InternalReadYamlObject() ??
                (YamlNode?)InternalReadYamlArray() ??
                (YamlNode?)InternalReadYamlValue();

            
        }

        internal YamlValue? InternalReadJsonValue()
        {
            var token = _lexer.PeekToken();
            if (token.Kind != YamlTokenKind.String &&
                token.Kind != YamlTokenKind.Literal)
            {
                return null;
            }

            if (token.Kind == YamlTokenKind.Literal)
            {
                var tokenTextString = token.Text.ToString();
                if (tokenTextString != "true" &&
                    tokenTextString != "false" &&
                    tokenTextString != "")
            }

            return new YamlValue(token.Text, token.Kind == YamlTokenKind.String);
        }

        internal YamlRef? InternalReadYamlRef()
        {

        }

        internal YamlNode InternalReadJson()
        {
            YamlToken token = _lexer.PeekToken();
            return token.Kind switch
            {
                YamlTokenKind.ObjectStart => InternalReadJsonObject()!,
                YamlTokenKind.ArrayStart => InternalReadJsonArray()!,
                YamlTokenKind.String => InternalReadJsonValue()!,
                YamlTokenKind.Literal => InternalReadJsonValue()!,
                _ => throw new InvalidOperationException("Unexpected token " + _lexer.PeekToken().Kind)
            };
        }

        /// <summary>
        /// Read a Yaml object
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public YamlObject ReadObject()
        {
            YamlToken peekedToken = _lexer.PeekToken();
            if (peekedToken.Kind != YamlTokenKind.ObjectStart)
                throw new InvalidOperationException("Unexpected token " + peekedToken.Kind);

            return InternalReadJsonObject();
        }

        /// <summary>
        /// Read a Yaml array
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public YamlArray ReadArray()
        {
            YamlToken peekedToken = _lexer.PeekToken();
            if (peekedToken.Kind != YamlTokenKind.ArrayStart)
                throw new InvalidOperationException("Unexpected token " + peekedToken.Kind);

            return InternalReadJsonArray();
        }

        /// <summary>
        /// Read a Yaml string
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public YamlValue ReadValue()
        {
            YamlToken token = _lexer.ReadToken();
            if (token.Kind != YamlTokenKind.String &&
                token.Kind != YamlTokenKind.Literal)
                throw new InvalidOperationException("Unexpected token " + token.Kind);

            return InternalReadYamlValue();
        }

        /// <summary>
        /// Read a Yaml data
        /// </summary>
        /// <returns></returns>
        public YamlNode Read()
        {
            return InternalReadJson();
        }

        public void Dispose()
        {
            _lexer.Dispose();
        }

        private record struct ObjectKeyAndAnchor(string key, string? anchorName);
    }
}