using System.Collections.Generic;
using EleCho.Compiling;
using EleCho.Yaml.Parsing.Syntaxes;

namespace EleCho.Yaml.Parsing.Grammars
{
    public class ConstructBlockMapping : Grammar<MappingPart, ISyntax, ISyntax>
    {
        public override bool CanConstruct(GrammarContext context, MappingPart input1, ISyntax input2)
        {
            return input2 is EndOfFile || input2.Position < input1.Indent;
        }

        public override IEnumerable<ISyntax> Construct(GrammarContext context, MappingPart input1, ISyntax input2)
        {
            yield return new BlockMapping(input1);
            yield return input2;
        }
    }
}
