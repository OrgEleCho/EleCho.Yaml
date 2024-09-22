using EleCho.Compiling;

namespace EleCho.Yaml.Parsing.Syntaxes
{
    public class ScalarPart : Syntax
    {
        public ScalarPart(
            CharSequence charSequence, bool isLiteral, bool isFolded, bool isSingleQuoted, bool isDoubleQuoted) 
            : base(charSequence)
        {
            IsLiteral = isLiteral;
            IsFolded = isFolded;
            IsSingleQuoted = isSingleQuoted;
            IsDoubleQuoted = isDoubleQuoted;
        }

        public bool IsLiteral { get; set; }
        public bool IsFolded { get; set; }
        public bool IsSingleQuoted { get; set; }
        public bool IsDoubleQuoted { get; set; }
        public bool IsQuoteEnded { get; set; }
        public bool IsEscaping { get; set; }
    }
}
