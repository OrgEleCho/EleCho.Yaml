using System.Collections.Generic;
using EleCho.Compiling;
using EleCho.Yaml.Parsing.Syntaxes;

namespace EleCho.Yaml.Parsing.Grammars
{
    public class RemoveAllLineBreak : Grammar<LineBreak, ISyntax>
    {
        public override bool CanConstruct(GrammarContext context, LineBreak input)
        {
            return true;
        }

        public override IEnumerable<ISyntax> Construct(GrammarContext context, LineBreak input)
        {
            yield break;
        }
    }
}
