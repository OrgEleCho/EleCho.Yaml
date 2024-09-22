using System.Collections.Generic;
using EleCho.Compiling;
using EleCho.Yaml.Parsing.Syntaxes;

namespace EleCho.Yaml.Parsing.Grammars
{
    public class CharSequenceToColon : Grammar<CharSequence, Colon>
    {
        public override bool CanConstruct(GrammarContext context, CharSequence input)
        {
            return YamlCharacters.IsMappingValue(input.Text.Span[0]);
        }

        public override IEnumerable<Colon> Construct(GrammarContext context, CharSequence input)
        {
            yield return new Colon(input);
        }
    }
}
