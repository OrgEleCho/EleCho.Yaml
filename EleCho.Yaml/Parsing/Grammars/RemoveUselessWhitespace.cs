using System.Collections.Generic;
using EleCho.Compiling;
using EleCho.Yaml.Parsing.Syntaxes;

namespace EleCho.Yaml.Parsing.Grammars
{
    public class RemoveUselessWhitespace : Grammar<ISyntax, WhiteSpace, ISyntax>
    {
        public override bool CanConstruct(GrammarContext context, ISyntax input1, WhiteSpace input2)
        {
            return input1 is not LineBreak;
        }

        public override IEnumerable<ISyntax> Construct(GrammarContext context, ISyntax input1, WhiteSpace input2)
        {
            yield return input1;
        }
    }
}
