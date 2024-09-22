using System.Collections.Generic;
using EleCho.Compiling;
using EleCho.Yaml.Parsing.Syntaxes;

namespace EleCho.Yaml.Parsing.Grammars
{
    public class CombineScalarDashScalarPart : Grammar<Scalar, Dash, ScalarPart, ScalarPart>
    {
        public override bool CanConstruct(GrammarContext context, Scalar input1, Dash input2, ScalarPart input3)
        {
            return input2.TailSpacing == 0;
        }

        public override IEnumerable<ScalarPart> Construct(GrammarContext context, Scalar input1, Dash input2, ScalarPart input3)
        {
            input3.ExpandTextRange(input1);
            yield return input3;
        }
    }
}
