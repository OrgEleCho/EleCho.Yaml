using System.Collections.Generic;
using EleCho.Compiling;
using EleCho.Yaml.Parsing.Syntaxes;

namespace EleCho.Yaml.Parsing.Grammars
{
    public class ConstructBlockSequenceItemFromScalar : Grammar<Dash, Scalar, BlockSequenceItem>
    {
        public override bool CanConstruct(GrammarContext context, Dash input1, Scalar input2)
        {
            return input1.TailSpacing > 0;
        }

        public override IEnumerable<BlockSequenceItem> Construct(GrammarContext context, Dash input1, Scalar input2)
        {
            yield return new BlockSequenceItem(input1, input2);
        }
    }

    public class ConstructBlockSequenceItemFromScalarAndOther : Grammar<Dash, Scalar, ISyntax, ISyntax>
    {
        public override bool CanConstruct(GrammarContext context, Dash input1, Scalar input2, ISyntax input3)
        {
            return input1.TailSpacing > 0;
        }

        public override IEnumerable<ISyntax> Construct(GrammarContext context, Dash input1, Scalar input2, ISyntax input3)
        {
            yield return new BlockSequenceItem(input1, input2);
            yield return input3;
        }
    }
}
