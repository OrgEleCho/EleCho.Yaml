using System.Collections.Generic;
using EleCho.Compiling;
using EleCho.Yaml.Parsing.Syntaxes;

namespace EleCho.Yaml.Parsing.Grammars
{
    public class RemoveAllWhiteSpace : Grammar<WhiteSpace, ISyntax>
    {
        public override bool CanConstruct(GrammarContext context, WhiteSpace input)
        {
            return true;
        }

        public override IEnumerable<ISyntax> Construct(GrammarContext context, WhiteSpace input)
        {
            yield break;
        }
    }
}
