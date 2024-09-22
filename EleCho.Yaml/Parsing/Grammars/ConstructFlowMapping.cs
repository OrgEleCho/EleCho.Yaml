using System.Collections.Generic;
using EleCho.Compiling;
using EleCho.Yaml.Parsing.Syntaxes;

namespace EleCho.Yaml.Parsing.Grammars
{
    public class ConstructFlowMapping : Grammar<FlowMappingStart, MappingPart, FlowMappingEnd, FlowMapping>
    {
        public override bool CanConstruct(GrammarContext context, FlowMappingStart input1, MappingPart input2, FlowMappingEnd input3)
        {
            return true;
        }

        public override IEnumerable<FlowMapping> Construct(GrammarContext context, FlowMappingStart input1, MappingPart input2, FlowMappingEnd input3)
        {
            yield return new FlowMapping(input1, input2, input3);
        }
    }
}
