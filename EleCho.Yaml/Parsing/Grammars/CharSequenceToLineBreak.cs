using System;
using System.Collections.Generic;
using EleCho.Compiling;
using EleCho.Yaml.Parsing.Syntaxes;

namespace EleCho.Yaml.Parsing.Grammars
{
    public class CharSequenceToLineBreak : Grammar<CharSequence, LineBreak>
    {
        public override bool CanConstruct(GrammarContext context, CharSequence input)
        {
            if (input.TextLength > 1)
            {
                return false;
            }

            char c = input.Text.Span[0];

            return YamlCharacters.IsLineBreak(c);
        }

        public override IEnumerable<LineBreak> Construct(GrammarContext context, CharSequence input)
        {
            yield return new LineBreak(input);
        }
    }
}
