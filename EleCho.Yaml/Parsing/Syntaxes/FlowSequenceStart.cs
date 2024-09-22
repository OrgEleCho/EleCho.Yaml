using EleCho.Compiling;

namespace EleCho.Yaml.Parsing.Syntaxes
{
    public class FlowSequenceStart : Syntax
    {
        public int TailSpacing => TextLength - 1;

        public FlowSequenceStart(CharSequence charSequence)
            : base(charSequence)
        {

        }
    }
}
