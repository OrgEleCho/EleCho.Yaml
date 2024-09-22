using System.Collections;
using System.Collections.Generic;

namespace EleCho.Compiling
{
    public static class SyntaxUtilities
    {
        public static void GetSyntaxInfo(
            ISyntax selfSyntax, 
            IEnumerable<ISyntax> syntaxes,
            out int start, 
            out int end, 
            out int lineNumber, 
            out int position,
            out int endPosition)
        {
            start = selfSyntax.TextStart;
            end = selfSyntax.TextEnd;
            lineNumber = selfSyntax.LineNumber;
            position = selfSyntax.Position;
            endPosition = selfSyntax.EndPosition;

            foreach (var syntax in syntaxes)
            {
                if (syntax.TextStart < start)
                {
                    start = syntax.TextStart;
                    lineNumber = syntax.LineNumber;
                    position = syntax.Position;
                }

                if (syntax.TextEnd > end)
                {
                    end = syntax.TextEnd;
                    endPosition = syntax.EndPosition;
                }
            }
        }

        public static void GetSyntaxInfo(
            IEnumerable<ISyntax> syntaxes, 
            out int start, 
            out int end, 
            out int lineNumber, 
            out int position,
            out int endPosition)
        {
            start = int.MaxValue;
            end = int.MinValue;
            lineNumber = 0;
            position = 0;
            endPosition = 0;

            foreach (var syntax in syntaxes)
            {
                if (syntax.TextStart < start)
                {
                    start = syntax.TextStart;
                    lineNumber = syntax.LineNumber;
                    position = syntax.Position;
                }

                if (syntax.TextEnd > end)
                {
                    end = syntax.TextEnd;
                    endPosition = syntax.EndPosition;
                }
            }
        }
    }
}
