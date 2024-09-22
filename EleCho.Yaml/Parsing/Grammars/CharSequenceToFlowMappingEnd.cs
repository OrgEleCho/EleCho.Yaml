using System.Collections.Generic;
using EleCho.Compiling;
using EleCho.Yaml.Parsing.Syntaxes;

namespace EleCho.Yaml.Parsing.Grammars
{
    public class CharSequenceToFlowMappingEnd : Grammar<CharSequence, FlowMappingEnd>
    {
        public override bool CanConstruct(GrammarContext context, CharSequence input)
        {
            return YamlCharacters.IsMappingEnd(input.Text.Span[0]);
        }

        public override IEnumerable<FlowMappingEnd> Construct(GrammarContext context, CharSequence input)
        {
            yield return new FlowMappingEnd(input);
        }
    }
}
