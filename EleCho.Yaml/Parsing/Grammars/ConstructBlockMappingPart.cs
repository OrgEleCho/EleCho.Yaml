using System.Collections.Generic;
using EleCho.Compiling;
using EleCho.Yaml.Parsing.Syntaxes;

namespace EleCho.Yaml.Parsing.Grammars
{
    public class ConstructBlockMappingPart : Grammar<MappingItem, MappingPart>
    {
        public override bool CanConstruct(GrammarContext context, MappingItem input)
        {
            return true;
        }

        public override IEnumerable<MappingPart> Construct(GrammarContext context, MappingItem input)
        {
            yield return new MappingPart(input);
        }
    }

    public class ConstructBlockMappingPartAndOther : Grammar<MappingItem, ISyntax, ISyntax>
    {
        public override bool CanConstruct(GrammarContext context, MappingItem input, ISyntax input2)
        {
            return true;
        }

        public override IEnumerable<ISyntax> Construct(GrammarContext context, MappingItem input, ISyntax input2)
        {
            yield return new MappingPart(input);
            yield return input2;
        }
    }
}
