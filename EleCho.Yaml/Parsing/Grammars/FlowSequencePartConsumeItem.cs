using System.Collections.Generic;
using EleCho.Compiling;
using EleCho.Yaml.Parsing.Syntaxes;

namespace EleCho.Yaml.Parsing.Grammars
{
    public class FlowSequencePartConsumeItem : Grammar<FlowSequencePart, Comma, FlowSequenceItem, FlowSequencePart>
    {
        public override bool CanConstruct(GrammarContext context, FlowSequencePart input1, Comma input2, FlowSequenceItem input3)
        {
            return true;
        }

        public override IEnumerable<FlowSequencePart> Construct(GrammarContext context, FlowSequencePart input1, Comma input2, FlowSequenceItem input3)
        {
            input1.AddItem(input3);
            yield return input1;
        }
    }
}
