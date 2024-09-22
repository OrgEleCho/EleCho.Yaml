using System.Collections.Generic;
using EleCho.Compiling;
using EleCho.Yaml.Parsing.Syntaxes;

namespace EleCho.Yaml.Parsing.Grammars
{
    public class CombineBlockMappingKeyScalarPart : Grammar<MappingKey, ScalarPart, ScalarPart>
    {
        public override bool CanConstruct(GrammarContext context, MappingKey input1, ScalarPart input2)
        {
            return input1.TailSpacing == 0;
        }

        public override IEnumerable<ScalarPart> Construct(GrammarContext context, MappingKey input1, ScalarPart input2)
        {
            input2.ExpandTextRange(input1);
            yield return input2;
        }
    }
}
