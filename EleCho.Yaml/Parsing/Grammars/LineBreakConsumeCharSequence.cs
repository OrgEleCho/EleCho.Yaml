using System.Collections.Generic;
using EleCho.Compiling;
using EleCho.Yaml.Parsing.Syntaxes;

namespace EleCho.Yaml.Parsing.Grammars
{
    public class LineBreakConsumeCharSequence : Grammar<LineBreak, CharSequence, LineBreak>
    {
        public override bool CanConstruct(GrammarContext context, LineBreak input1, CharSequence input2) => YamlCharacters.IsLineBreak(input2.Text.Span[0]);
        public override IEnumerable<LineBreak> Construct(GrammarContext context, LineBreak input1, CharSequence input2)
        {
            input1.ExpandTextRange(input2);
            yield return input1;
        }
    }
}
