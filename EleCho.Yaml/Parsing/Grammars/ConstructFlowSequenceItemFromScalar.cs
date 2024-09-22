using System.Collections.Generic;
using EleCho.Compiling;
using EleCho.Yaml.Parsing.Syntaxes;

namespace EleCho.Yaml.Parsing.Grammars
{
    public class ConstructFlowSequenceItemFromScalar : Grammar<Scalar, FlowSequenceItem>
    {
        public override bool CanConstruct(GrammarContext context, Scalar input)
        {
            if (context.SyntaxCountBefore < 1 ||
                context.GetSyntaxBefore(0) is not FlowSequenceStart or FlowSequencePart)
            {
                return false;
            }

            return true;
        }

        public override IEnumerable<FlowSequenceItem> Construct(GrammarContext context, Scalar input)
        {
            yield return new FlowSequenceItem(input);
        }
    }
}
