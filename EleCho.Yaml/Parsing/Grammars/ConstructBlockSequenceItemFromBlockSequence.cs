using System.Collections.Generic;
using EleCho.Compiling;
using EleCho.Yaml.Parsing.Syntaxes;

namespace EleCho.Yaml.Parsing.Grammars
{
    public class ConstructBlockSequenceItemFromBlockSequence : Grammar<Dash, BlockSequence, BlockSequenceItem>
    {
        public override bool CanConstruct(GrammarContext context, Dash input1, BlockSequence input2)
        {
            return input2.Indent > input1.Position;
        }

        public override IEnumerable<BlockSequenceItem> Construct(GrammarContext context, Dash input1, BlockSequence input2)
        {
            yield return new BlockSequenceItem(input1, input2);
        }
    }

    public class ConstructBlockSequenceItemFromBlockSequenceAndOther : Grammar<Dash, BlockSequence, ISyntax, ISyntax>
    {
        public override bool CanConstruct(GrammarContext context, Dash input1, BlockSequence input2, ISyntax input3)
        {
            return input2.Indent > input1.Position;
        }

        public override IEnumerable<ISyntax> Construct(GrammarContext context, Dash input1, BlockSequence input2, ISyntax input3)
        {
            yield return new BlockSequenceItem(input1, input2);
            yield return input3;
        }
    }
}
