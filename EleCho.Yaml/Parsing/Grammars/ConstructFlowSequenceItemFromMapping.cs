using System.Collections.Generic;
using EleCho.Compiling;
using EleCho.Yaml.Parsing.Syntaxes;

namespace EleCho.Yaml.Parsing.Grammars
{
    public class ConstructFlowSequenceItemFromMapping : Grammar<FlowMapping, FlowSequenceItem>
    {
        public override bool CanConstruct(GrammarContext context, FlowMapping input)
        {
            if (context.SyntaxCountBefore < 0 ||
                context.GetSyntaxBefore(0) is not FlowSequenceStart or FlowSequencePart)
            {
                return false;
            }

            return true;
        }

        public override IEnumerable<FlowSequenceItem> Construct(GrammarContext context, FlowMapping input)
        {
            yield return new FlowSequenceItem(input);
        }
    }
}
