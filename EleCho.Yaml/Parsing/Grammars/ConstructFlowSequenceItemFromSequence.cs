using System.Collections.Generic;
using EleCho.Compiling;
using EleCho.Yaml.Parsing.Syntaxes;

namespace EleCho.Yaml.Parsing.Grammars
{
    public class ConstructFlowSequenceItemFromSequence : Grammar<FlowSequence, FlowSequenceItem>
    {
        public override bool CanConstruct(GrammarContext context, FlowSequence input)
        {
            if (context.SyntaxCountBefore < 1 ||
                context.GetSyntaxBefore(0) is not FlowSequenceStart or FlowSequencePart)
            {
                return false;
            }

            return true;
        }

        public override IEnumerable<FlowSequenceItem> Construct(GrammarContext context, FlowSequence input)
        {
            yield return new FlowSequenceItem(input);
        }
    }
}
