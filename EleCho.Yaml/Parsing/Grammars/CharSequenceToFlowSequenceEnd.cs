using System.Collections.Generic;
using EleCho.Compiling;
using EleCho.Yaml.Parsing.Syntaxes;

namespace EleCho.Yaml.Parsing.Grammars
{
    public class CharSequenceToFlowSequenceEnd : Grammar<CharSequence, FlowSequenceEnd>
    {
        public override bool CanConstruct(GrammarContext context, CharSequence input)
        {
            return YamlCharacters.IsSequenceEnd(input.Text.Span[0]);
        }

        public override IEnumerable<FlowSequenceEnd> Construct(GrammarContext context, CharSequence input)
        {
            yield return new FlowSequenceEnd(input);
        }
    }
}
