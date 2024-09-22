using System.Collections.Generic;
using EleCho.Compiling;
using EleCho.Yaml.Parsing.Syntaxes;

namespace EleCho.Yaml.Parsing.Grammars
{
    public class ConstructBlockMappingItemFromBlockMapping : Grammar<MappingKey, BlockMapping, MappingItem>
    {
        public override bool CanConstruct(GrammarContext context, MappingKey input1, BlockMapping input2)
        {
            if (context.SyntaxCountBefore < 0 ||
                context.GetSyntaxBefore(0) is not FlowMappingStart)
            {
                return false;
            }

            return input2.Indent > input1.Position;
        }

        public override IEnumerable<MappingItem> Construct(GrammarContext context, MappingKey input1, BlockMapping input2)
        {
            yield return new MappingItem(input1, input2);
        }
    }

    public class ConstructBlockMappingItemFromBlockMappingAndOther : Grammar<MappingKey, BlockMapping, ISyntax, ISyntax>
    {
        public override bool CanConstruct(GrammarContext context, MappingKey input1, BlockMapping input2, ISyntax input3)
        {
            if (context.SyntaxCountBefore < 0 ||
                context.GetSyntaxBefore(0) is not FlowMappingStart)
            {
                return false;
            }

            return input2.Indent > input1.Position;
        }

        public override IEnumerable<ISyntax> Construct(GrammarContext context, MappingKey input1, BlockMapping input2, ISyntax input3)
        {
            yield return new MappingItem(input1, input2);
            yield return input3;
        }
    }
}
