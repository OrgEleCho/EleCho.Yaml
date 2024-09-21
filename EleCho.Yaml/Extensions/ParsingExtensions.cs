using System;
using System.Collections.Generic;
using System.Text;
using EleCho.Yaml.Parsing;
using EleCho.Yaml.Utilities;

namespace EleCho.Yaml.Extensions
{
    internal static class ParsingExtensions
    {
        public static YamlToken ReadTokenAndEnsureKind(this YamlBufferedLexer lexer, YamlTokenKind kind)
        {
            var token = lexer.ReadToken();

            if (token.Kind != kind)
            {
                throw new YamlException($"Unexpected token: {token}");
            }

            return token;
        }

        public static YamlToken ReadTokenOrDefault(this YamlBufferedLexer lexer, Predicate<YamlToken> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            var token = lexer.PeekToken();
            if (predicate.Invoke(token))
            {
                lexer.ReadToken();
                return token;
            }

            return default;
        }

        public static YamlToken ReadTokenOrDefault(this YamlBufferedLexer lexer, YamlTokenKind kind)
        {
            var token = lexer.PeekToken();
            if (token.Kind == kind)
            {
                lexer.ReadToken();
                return token;
            }

            return default;
        }
    }
}
