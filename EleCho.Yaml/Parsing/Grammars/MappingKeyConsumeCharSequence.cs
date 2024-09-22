using System.Collections.Generic;
using EleCho.Compiling;
using EleCho.Yaml.Parsing.Syntaxes;

namespace EleCho.Yaml.Parsing.Grammars
{
    public class MappingKeyConsumeCharSequence : Grammar<MappingKey, CharSequence, MappingKey>
    {
        public override bool CanConstruct(GrammarContext context, MappingKey input1, CharSequence input2)
        {
            return YamlCharacters.IsWhiteSpace(input2.Text.Span[0]);
        }

        public override IEnumerable<MappingKey> Construct(GrammarContext context, MappingKey input1, CharSequence input2)
        {
            input1.ExpandTextRange(input2);
            yield return input1;
        }
    }
}
