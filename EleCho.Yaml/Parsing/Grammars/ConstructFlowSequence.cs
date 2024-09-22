using System.Collections.Generic;
using EleCho.Compiling;
using EleCho.Yaml.Parsing.Syntaxes;

namespace EleCho.Yaml.Parsing.Grammars
{
    public class ConstructFlowSequence : Grammar<FlowSequenceStart, FlowSequencePart, FlowSequenceEnd, FlowSequence>
    {
        public override bool CanConstruct(GrammarContext context, FlowSequenceStart input1, FlowSequencePart input2, FlowSequenceEnd input3)
        {
            return true;
        }

        public override IEnumerable<FlowSequence> Construct(GrammarContext context, FlowSequenceStart input1, FlowSequencePart input2, FlowSequenceEnd input3)
        {
            yield return new FlowSequence(input1, input2, input3);
        }
    }
}
