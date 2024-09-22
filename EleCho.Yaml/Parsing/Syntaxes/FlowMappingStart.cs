using EleCho.Compiling;

namespace EleCho.Yaml.Parsing.Syntaxes
{
    public class FlowMappingStart : Syntax
    {
        public int TailSpacing => TextLength - 1;

        public FlowMappingStart(CharSequence charSequence)
            : base(charSequence)
        {

        }
    }
}
