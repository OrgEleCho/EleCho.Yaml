using System.Collections.Generic;
using EleCho.Compiling;
using EleCho.Yaml.Parsing.Syntaxes;

namespace EleCho.Yaml.Parsing.Grammars
{
    public class CombineDashScalarPart : Grammar<Dash, ScalarPart, ScalarPart>
    {
        public override bool CanConstruct(GrammarContext context, Dash input1, ScalarPart input2)
        {
            return input1.TailSpacing == 0;
        }

        public override IEnumerable<ScalarPart> Construct(GrammarContext context, Dash input1, ScalarPart input2)
        {
            input2.ExpandTextRange(input1);
            yield return input2;
        }
    }
}
