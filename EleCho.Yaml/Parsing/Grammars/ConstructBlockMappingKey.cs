using System.Collections.Generic;
using EleCho.Compiling;
using EleCho.Yaml.Parsing.Syntaxes;

namespace EleCho.Yaml.Parsing.Grammars
{
    public class ConstructBlockMappingKey : Grammar<Scalar, Colon, MappingKey>
    {
        public override bool CanConstruct(GrammarContext context, Scalar input1, Colon input2)
        {
            return true;
        }

        public override IEnumerable<MappingKey> Construct(GrammarContext context, Scalar input1, Colon input2)
        {
            yield return new MappingKey(input1, input2);
        }
    }
}
