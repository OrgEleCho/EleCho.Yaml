using System;
using EleCho.Compiling;
using EleCho.Yaml.Parsing.Grammars;

namespace EleCho.Yaml.Parsing
{
    internal class YamlParser : Parser
    {
        public YamlParser(ReadOnlyMemory<char> input, YamlParserOptions options)
            : base([
                // 词法叠加
                new DashConsumeCharSequence(),
                new WhiteSpaceConsumeCharSequence(),
                new LineBreakConsumeCharSequence(),
                new MappingKeyConsumeCharSequence(),
                new ScalarPartConsumeCharSequence(),
                new ConstructQuotedScalar(),

                // 语法叠加
                new MappingPartConsumeItem(),
                new BlockSequencePartConsumeItem(),
                new FlowSequencePartConsumeItem(),
                
                // 词法构建
                new CharSequenceToWhiteSpace(),
                new CharSequenceToLineBreak(),
                new CharSequenceToColon(),
                new CharSequenceToDash(),
                new CharSequenceToFlowMappingStart(),
                new CharSequenceToFlowMappingEnd(),
                new CharSequenceToFlowSequenceStart(),
                new CharSequenceToFlowSequenceEnd(),
                new CharSequenceToScalar(),

                // 词法转换
                new ConvertScalar(),
                new CombineScalarDashScalarPart(),
                new CombineColonScalarPart(),
                new CombineDashScalarPart(),

                // 移除多余的东西
                new RemoveAllWhiteSpace(),
                new RemoveAllLineBreak(),

                // 语法构建
                new ConstructBlockMappingKey(),


                new ConstructBlockMappingItemFromBlockMapping(),
                new ConstructBlockMappingItemFromBlockSequence(),
                new ConstructBlockMappingItemFromScalar(),
                new ConstructBlockSequenceItemFromBlockMapping(),
                new ConstructBlockSequenceItemFromBlockSequence(),
                new ConstructBlockSequenceItemFromScalar(),

                new ConstructBlockMappingItemFromBlockMappingAndOther(),
                new ConstructBlockMappingItemFromBlockSequenceAndOther(),
                new ConstructBlockMappingItemFromScalarAndOther(),
                new ConstructBlockSequenceItemFromBlockMappingAndOther(),
                new ConstructBlockSequenceItemFromBlockSequenceAndOther(),
                new ConstructBlockSequenceItemFromScalarAndOther(),

                new ConstructFlowSequenceItemFromMapping(),
                new ConstructFlowSequenceItemFromSequence(),
                new ConstructFlowSequenceItemFromScalar(),

                new ConstructBlockMappingPart(),
                new ConstructBlockSequencePart(),

                new ConstructFlowSequencePart(),

                new ConstructBlockMappingPartAndOther(),
                new ConstructBlockSequencePartAndOther(),

                new ConstructBlockMapping(),
                new ConstructBlockSequence(),

                new ConstructFlowMapping(),
                new ConstructEmptyFlowMapping(),
                new ConstructFlowSequence(),
                new ConstructEmptyFlowSequence(),

                // 语法转换
                new CombineBlockMappingKeyScalarPart(),

            ], input)
        {

        }
    }
}
