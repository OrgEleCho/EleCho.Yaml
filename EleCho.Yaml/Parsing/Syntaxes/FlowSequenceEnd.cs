using EleCho.Compiling;

namespace EleCho.Yaml.Parsing.Syntaxes
{
    public class FlowSequenceEnd : Syntax
    {
        public int TailSpacing => TextLength - 1;

        public FlowSequenceEnd(CharSequence charSequence)
            : base(charSequence)
        {

        }
    }
}
