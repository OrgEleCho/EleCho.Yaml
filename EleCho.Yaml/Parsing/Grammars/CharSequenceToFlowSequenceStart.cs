using System.Collections.Generic;
using EleCho.Compiling;
using EleCho.Yaml.Parsing.Syntaxes;

namespace EleCho.Yaml.Parsing.Grammars
{
    public class CharSequenceToFlowSequenceStart : Grammar<CharSequence, FlowSequenceStart>
    {
        public override bool CanConstruct(GrammarContext context, CharSequence input)
        {
            return YamlCharacters.IsSequenceStart(input.Text.Span[0]);
        }

        public override IEnumerable<FlowSequenceStart> Construct(GrammarContext context, CharSequence input)
        {
            yield return new FlowSequenceStart(input);
        }
    }
}
