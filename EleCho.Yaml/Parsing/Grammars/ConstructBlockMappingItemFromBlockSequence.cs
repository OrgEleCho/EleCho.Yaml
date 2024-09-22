using System.Collections.Generic;
using EleCho.Compiling;
using EleCho.Yaml.Parsing.Syntaxes;

namespace EleCho.Yaml.Parsing.Grammars
{
    public class ConstructBlockMappingItemFromBlockSequence : Grammar<MappingKey, BlockSequence, MappingItem>
    {
        public override bool CanConstruct(GrammarContext context, MappingKey input1, BlockSequence input2)
        {
            return input2.Indent > input1.Position;
        }

        public override IEnumerable<MappingItem> Construct(GrammarContext context, MappingKey input1, BlockSequence input2)
        {
            yield return new MappingItem(input1, input2);
        }
    }

    public class ConstructBlockMappingItemFromBlockSequenceAndOther : Grammar<MappingKey, BlockSequence, ISyntax, ISyntax>
    {
        public override bool CanConstruct(GrammarContext context, MappingKey input1, BlockSequence input2, ISyntax input3)
        {
            return input2.Indent > input1.Position;
        }

        public override IEnumerable<ISyntax> Construct(GrammarContext context, MappingKey input1, BlockSequence input2, ISyntax input3)
        {
            yield return new MappingItem(input1, input2);
            yield return input3;
        }
    }
}
