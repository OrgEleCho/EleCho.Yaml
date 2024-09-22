using System.Collections.Generic;
using EleCho.Compiling;
using EleCho.Yaml.Parsing.Syntaxes;

namespace EleCho.Yaml.Parsing.Grammars
{
    public class ConstructBlockMappingItemFromScalar : Grammar<MappingKey, Scalar, MappingItem>
    {
        public override bool CanConstruct(GrammarContext context, MappingKey input1, Scalar input2)
        {
            return input1.TailSpacing > 0;
        }

        public override IEnumerable<MappingItem> Construct(GrammarContext context, MappingKey input1, Scalar input2)
        {
            yield return new MappingItem(input1, input2);
        }
    }

    public class ConstructBlockMappingItemFromScalarAndOther : Grammar<MappingKey, Scalar, ISyntax, ISyntax>
    {
        public override bool CanConstruct(GrammarContext context, MappingKey input1, Scalar input2, ISyntax input3)
        {
            return input1.TailSpacing > 0;
        }

        public override IEnumerable<ISyntax> Construct(GrammarContext context, MappingKey input1, Scalar input2, ISyntax input3)
        {
            yield return new MappingItem(input1, input2);
            yield return input3;
        }
    }
}
