using System;

namespace EleCho.Yaml.Parsing
{
    internal record struct YamlToken
    {
        public YamlToken(YamlTokenKind tokenType, ReadOnlyMemory<char> text, int line, int position)
        {
            Kind = tokenType;
            Text = text;
            Line = line;
            Position = position;
        }

        public bool IsNone() => Kind == YamlTokenKind.None;
        public bool IsNotNone() => Kind != YamlTokenKind.None;

        public YamlTokenKind Kind { get; }
        public ReadOnlyMemory<char> Text { get; }
        public int Line { get; }
        public int Position { get; }

        public bool Is(YamlTokenKind kind) => Kind == kind;

        public static YamlToken None => new YamlToken(YamlTokenKind.None, default, 0, 0);
    }
}
