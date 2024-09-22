using System.Collections.Generic;
using EleCho.Compiling;
using EleCho.Yaml.Parsing.Syntaxes;

namespace EleCho.Yaml.Parsing.Grammars
{
    public class ConvertScalar : Grammar<ScalarPart, ISyntax, ISyntax>
    {
        public override bool CanConstruct(GrammarContext context, ScalarPart input1, ISyntax input2)
        {
            // maybe end of file
            return input2 is not CharSequence;
        }

        public override IEnumerable<ISyntax> Construct(GrammarContext context, ScalarPart input1, ISyntax input2)
        {
            yield return new Scalar(input1);
            yield return input2;
        }
    }
}
