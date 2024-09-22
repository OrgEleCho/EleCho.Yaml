using EleCho.Compiling;

namespace EleCho.Yaml.Parsing.Syntaxes
{
    public class FlowMappingEnd : Syntax
    {
        public int TailSpacing => TextLength - 1;

        public FlowMappingEnd(CharSequence charSequence)
            : base(charSequence)
        {

        }
    }
}
