using EleCho.Compiling;

namespace EleCho.Yaml.Parsing.Syntaxes
{
    public class FlowSequenceItem : Syntax
    {
        public FlowSequenceItem(ISyntax value)
            : base(value)
        {
            Value = value;
        }

        public ISyntax Value { get; }
    }
}
