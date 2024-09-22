using System.Collections.Generic;
using EleCho.Compiling;
using EleCho.Yaml.Parsing.Syntaxes;

namespace EleCho.Yaml.Parsing.Grammars
{
    public class ConstructEmptyFlowSequence : Grammar<FlowSequenceStart, FlowSequenceEnd, FlowSequence>
    {
        public override bool CanConstruct(GrammarContext context, FlowSequenceStart input1, FlowSequenceEnd input2)
        {
            return true;
        }

        public override IEnumerable<FlowSequence> Construct(GrammarContext context, FlowSequenceStart input1, FlowSequenceEnd input2)
        {
            yield return new FlowSequence(input1, input2);
        }
    }
}
