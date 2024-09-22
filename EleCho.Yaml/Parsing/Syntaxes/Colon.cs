using EleCho.Compiling;

namespace EleCho.Yaml.Parsing.Syntaxes
{
    public class Colon : Syntax
    {
        public int TailSpacing => TextLength - 1;

        public Colon(CharSequence charSequence)
            : base(charSequence)
        {

        }
    }
}
