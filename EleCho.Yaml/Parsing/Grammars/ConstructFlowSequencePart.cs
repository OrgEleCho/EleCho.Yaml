using System.Collections.Generic;
using EleCho.Compiling;
using EleCho.Yaml.Parsing.Syntaxes;

namespace EleCho.Yaml.Parsing.Grammars
{
    public class ConstructFlowSequencePart : Grammar<FlowSequenceItem, FlowSequencePart>
    {
        public override bool CanConstruct(GrammarContext context, FlowSequenceItem input)
        {
            return true;
        }

        public override IEnumerable<FlowSequencePart> Construct(GrammarContext context, FlowSequenceItem input)
        {
            yield return new FlowSequencePart(input);
        }
    }
}
