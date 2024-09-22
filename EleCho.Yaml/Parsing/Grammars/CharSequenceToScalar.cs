using System;
using System.Collections.Generic;
using System.Text;
using EleCho.Compiling;
using EleCho.Yaml.Parsing.Syntaxes;

namespace EleCho.Yaml.Parsing.Grammars
{
    public class CharSequenceToScalar : Grammar<CharSequence, ISyntax>
    {
        public override bool CanConstruct(GrammarContext context, CharSequence input)
        {
            if (input.TextLength > 1)
            {
                return false;
            }

            char c = input.Text.Span[0];

            return
                YamlCharacters.IsLiteral(c) ||
                YamlCharacters.IsFolded(c) ||
                YamlCharacters.IsSingleQuote(c) ||
                YamlCharacters.IsDoubleQuote(c) ||
                YamlCharacters.IsCommonScalarStart(c) ;
        }

        public override IEnumerable<ISyntax> Construct(GrammarContext context, CharSequence input)
        {
            if (input.TextLength > 1)
            {
                throw new InvalidOperationException();
            }

            char c = input.Text.Span[0];

            if (YamlCharacters.IsLiteral(c))
            {
                yield return new Scalar(new ScalarPart(input, true, false, false, false));
            }
            else if (YamlCharacters.IsFolded(c))
            {
                yield return new Scalar(new ScalarPart(input, false, true, false, false));
            }
            else if (YamlCharacters.IsSingleQuote(c))
            {
                yield return new ScalarPart(input, false, false, true, false);
            }
            else if (YamlCharacters.IsDoubleQuote(c))
            {
                yield return new ScalarPart(input, false, false, false, true);
            }
            else if (YamlCharacters.IsCommonScalarStart(c))
            {
                yield return new ScalarPart(input, false, false, false, false);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }
}
