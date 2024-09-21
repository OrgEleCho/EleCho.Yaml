using System.Collections;
using System.Collections.Generic;

namespace EleCho.Compiler
{
    public static class SyntaxUtilities
    {
        public static void GetTextStartAndLength(IEnumerable<ISyntax> syntaxes, out int start, out int end)
        {
            int startIndex = int.MaxValue;
            int endIndex = int.MinValue;

            foreach (var syntax in syntaxes)
            {
                if (syntax.TextStart < startIndex)
                {
                    startIndex = syntax.TextStart;
                }

                if (syntax.TextEnd > endIndex)
                {
                    endIndex = syntax.TextEnd;
                }
            }

            start = startIndex;
            end = endIndex;
        }
    }
}
