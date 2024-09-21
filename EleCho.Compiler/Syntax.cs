using System;
using System.Linq;

namespace EleCho.Compiler
{
    public class Syntax : ISyntax
    {
        public ReadOnlyMemory<char> TextSource { get; }
        public int TextStart { get; }
        public int TextEnd { get; }

        public Syntax(params ISyntax[] syntaxes)
        {
            if (syntaxes.Length == 0)
            {
                throw new ArgumentException("Syntaxes can not be empty");
            }

            if (!syntaxes.All(s => s.TextSource.Equals(syntaxes[0].TextSource)))
            {
                throw new ArgumentException("Syntaxes must be with same text source");
            }

            SyntaxUtilities.GetTextStartAndLength(syntaxes, out var start, out var end);

            TextSource = syntaxes[0].TextSource;
            TextStart = start;
            TextEnd = end;
        }
    }
}
