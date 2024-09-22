using EleCho.Compiling;

namespace EleCho.Yaml.Parsing.Syntaxes
{
    public class Dash : Syntax
    {
        public int TailSpacing => TextLength - 1;

        public Dash(CharSequence charSequence)
            : base(charSequence)
        {

        }
    }
}
