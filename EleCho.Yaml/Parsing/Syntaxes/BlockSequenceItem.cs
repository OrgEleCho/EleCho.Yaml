using System;
using EleCho.Compiling;

namespace EleCho.Yaml.Parsing.Syntaxes
{
    public class BlockSequenceItem : Syntax
    {
        private readonly Dash _dash;

        public int Indent => _dash.Position;
        public ReadOnlyMemory<char> IndentText => TextSource.Slice(TextStart - Indent, Indent);

        public BlockSequenceItem(Dash dash, ISyntax value)
            : base(dash, value)
        {
            _dash = dash;
            Value = value;
        }

        public ISyntax Value { get; }
    }
}
