using System.Collections.Generic;
using EleCho.Compiling;
using EleCho.Yaml.Parsing.Syntaxes;

namespace EleCho.Yaml.Parsing.Grammars
{
    public class BlockSequencePartConsumeItem : Grammar<BlockSequencePart, BlockSequenceItem, BlockSequencePart>
    {
        public override bool CanConstruct(GrammarContext context, BlockSequencePart input1, BlockSequenceItem input2)
        {
            return input2.Indent >= input1.Indent;
        }

        public override IEnumerable<BlockSequencePart> Construct(GrammarContext context, BlockSequencePart input1, BlockSequenceItem input2)
        {
            if (input2.Indent > input1.Indent)
            {
                throw new YamlException("Invalid indent!")
                {
                    Index = input2.TextStart,
                    LineNumber = input2.LineNumber,
                    Position = input2.Position
                };
            }

            input1.AddItem(input2);
            yield return input1;
        }
    }

}
