using System.Collections.Generic;
using EleCho.Compiling;
using EleCho.Yaml.Parsing.Syntaxes;

namespace EleCho.Yaml.Parsing.Grammars
{
    public class ConstructBlockSequencePart : Grammar<BlockSequenceItem, BlockSequencePart>
    {
        public override bool CanConstruct(GrammarContext context, BlockSequenceItem input)
        {
            return true;
        }

        public override IEnumerable<BlockSequencePart> Construct(GrammarContext context, BlockSequenceItem input)
        {
            yield return new BlockSequencePart(input);
        }
    }

    public class ConstructBlockSequencePartAndOther : Grammar<BlockSequenceItem, ISyntax, ISyntax>
    {
        public override bool CanConstruct(GrammarContext context, BlockSequenceItem input, ISyntax input2)
        {
            return true;
        }

        public override IEnumerable<ISyntax> Construct(GrammarContext context, BlockSequenceItem input, ISyntax input2)
        {
            yield return new BlockSequencePart(input);
            yield return input2;
        }
    }
}
