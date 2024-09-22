using System.Collections.Generic;
using EleCho.Compiling;
using EleCho.Yaml.Parsing.Syntaxes;

namespace EleCho.Yaml.Parsing.Grammars
{
    public class ConstructEmptyFlowMapping : Grammar<FlowMappingStart, FlowMappingEnd, FlowMapping>
    {
        public override bool CanConstruct(GrammarContext context, FlowMappingStart input1, FlowMappingEnd input2)
        {
            return true;
        }

        public override IEnumerable<FlowMapping> Construct(GrammarContext context, FlowMappingStart input1, FlowMappingEnd input2)
        {
            yield return new FlowMapping(input1, input2);
        }
    }
}
