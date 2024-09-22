using System.Collections.Generic;
using EleCho.Compiling;
using EleCho.Yaml.Parsing.Syntaxes;

namespace EleCho.Yaml.Parsing.Grammars
{
    public class ConstructBlockSequence : Grammar<BlockSequencePart, ISyntax, ISyntax>
    {
        public override bool CanConstruct(GrammarContext context, BlockSequencePart input1, ISyntax input2)
        {
            return input2 is EndOfFile || input2.Position < input1.Indent;
        }

        public override IEnumerable<ISyntax> Construct(GrammarContext context, BlockSequencePart input1, ISyntax input2)
        {
            yield return new BlockSequence(input1);
            yield return input2;
        }
    }
}
