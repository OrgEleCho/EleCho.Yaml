using System;
using System.Collections.Generic;
using System.Text;
using EleCho.Compiling;

namespace EleCho.Yaml.Parsing.Syntaxes
{
    public class Scalar : Syntax
    {
        public Scalar(ScalarPart part)
            : base(part)
        {
            IsLiteral = part.IsLiteral;
            IsFolded = part.IsFolded;
            IsSingleQuoted = part.IsSingleQuoted;
            IsDoubleQuoted = part.IsDoubleQuoted;
        }

        public bool IsLiteral { get; set; }
        public bool IsFolded { get; set; }
        public bool IsSingleQuoted { get; set; }
        public bool IsDoubleQuoted { get; set; }

        public bool HasWhiteSpace
        {
            get
            {
                foreach (var c in Text.Span)
                {
                    if (YamlCharacters.IsWhiteSpace(c))
                    {
                        return true;
                    }
                }

                return false;
            }
        }
    }
}
