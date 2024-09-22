using System;
using System.Collections.Generic;
using EleCho.Compiling;
using EleCho.Yaml.Parsing.Syntaxes;

namespace EleCho.Yaml.Parsing.Grammars
{


    public class MultilineScalarPartConsumeCharSequence : Grammar<ScalarPart, CharSequence, ScalarPart>
    {
        public override bool CanConstruct(GrammarContext context, ScalarPart input1, CharSequence input2)
        {
            if (context.SyntaxCountBefore < 2)
            {
                return false;
            }

            var syntax2Before = context.GetSyntaxBefore(1);

            return input2.Position >= syntax2Before.EndPosition;
        }

        public override IEnumerable<ScalarPart> Construct(GrammarContext context, ScalarPart input1, CharSequence input2)
        {
            input1.ExpandTextRange(input2);
            yield return input1;
        }
    }
}
