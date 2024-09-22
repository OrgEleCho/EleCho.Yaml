using System.Collections.Generic;
using EleCho.Compiling;
using EleCho.Yaml.Parsing.Syntaxes;

namespace EleCho.Yaml.Parsing.Grammars
{
    public class CharSequenceToFlowMappingStart : Grammar<CharSequence, FlowMappingStart>
    {
        public override bool CanConstruct(GrammarContext context, CharSequence input)
        {
            return YamlCharacters.IsMappingStart(input.Text.Span[0]);
        }

        public override IEnumerable<FlowMappingStart> Construct(GrammarContext context, CharSequence input)
        {
            yield return new FlowMappingStart(input);
        }
    }
}
