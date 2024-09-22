using System.Collections.Generic;
using EleCho.Compiling;
using EleCho.Yaml.Parsing.Syntaxes;

namespace EleCho.Yaml.Parsing.Grammars
{
    public class CharSequenceToDash : Grammar<CharSequence, Dash>
    {
        public override bool CanConstruct(GrammarContext context, CharSequence input)
        {
            return YamlCharacters.IsSequenceEntry(input.Text.Span[0]);
        }

        public override IEnumerable<Dash> Construct(GrammarContext context, CharSequence input)
        {
            yield return new Dash(input);
        }
    }
}
