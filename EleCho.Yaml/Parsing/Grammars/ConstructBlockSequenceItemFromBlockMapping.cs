using System.Collections.Generic;
using EleCho.Compiling;
using EleCho.Yaml.Parsing.Syntaxes;

namespace EleCho.Yaml.Parsing.Grammars
{
    public class ConstructBlockSequenceItemFromBlockMapping : Grammar<Dash, BlockMapping, BlockSequenceItem>
    {
        public override bool CanConstruct(GrammarContext context, Dash input1, BlockMapping input2)
        {
            return input2.Indent > input1.Position;
        }

        public override IEnumerable<BlockSequenceItem> Construct(GrammarContext context, Dash input1, BlockMapping input2)
        {
            yield return new BlockSequenceItem(input1, input2);
        }
    }

    public class ConstructBlockSequenceItemFromBlockMappingAndOther : Grammar<Dash, BlockMapping, ISyntax, ISyntax>
    {
        public override bool CanConstruct(GrammarContext context, Dash input1, BlockMapping input2, ISyntax input3)
        {
            return input2.Indent > input1.Position;
        }

        public override IEnumerable<ISyntax> Construct(GrammarContext context, Dash input1, BlockMapping input2, ISyntax input3)
        {
            yield return new BlockSequenceItem(input1, input2);
            yield return input3;
        }
    }
}
