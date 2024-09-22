using System;
using System.Collections.Generic;
using EleCho.Compiling;
using EleCho.Yaml.Parsing.Syntaxes;

namespace EleCho.Yaml.Parsing.Grammars
{
    public class ScalarPartConsumeCharSequence : Grammar<ScalarPart, CharSequence, ScalarPart>
    {
        public override bool CanConstruct(GrammarContext context, ScalarPart input1, CharSequence input2)
        {
            char c = input2.Text.Span[0];

            if (input1.IsLiteral ||
                input1.IsFolded)
            {
                return false;
            }

            if (input1.IsSingleQuoted ||
                input1.IsDoubleQuoted)
            {
                return input1.IsEscaping;
            }

            return YamlCharacters.IsCommonScalarContent(c);
        }

        public override IEnumerable<ScalarPart> Construct(GrammarContext context, ScalarPart input1, CharSequence input2)
        {
            input1.ExpandTextRange(input2);
            yield return input1;
        }
    }
}
