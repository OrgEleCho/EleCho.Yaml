using System;
using System.Collections.Generic;
using EleCho.Compiling;
using EleCho.Yaml.Parsing.Extensions;
using EleCho.Yaml.Parsing.Syntaxes;

namespace EleCho.Yaml.Parsing.Grammars
{
    public class MappingPartConsumeItem : Grammar<MappingPart, MappingItem, MappingPart>
    {
        public override bool CanConstruct(GrammarContext context, MappingPart input1, MappingItem input2)
        {
            if (context.IsInFlow())
            {
                return true;
            }

            return input2.Indent >= input1.Indent;
        }

        public override IEnumerable<MappingPart> Construct(GrammarContext context, MappingPart input1, MappingItem input2)
        {
            if (!context.IsInFlow() && input2.Indent > input1.Indent)
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
