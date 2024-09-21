using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EleCho.Compiler
{
    public class CharSequence : ISyntax
    {
        public ReadOnlyMemory<char> TextSource { get; }
        public int TextStart { get; }
        public int TextEnd { get; set; }
        public int TextLength => TextEnd - TextStart;
        public int LineNumber { get; }
        public int Position { get; }

        public CharSequence(ReadOnlyMemory<char> textSource, int textStart, int textEnd, int lineNumber, int position)
        {
            TextSource = textSource;
            TextStart = textStart;
            TextEnd = textEnd;

            LineNumber = lineNumber;
            Position = position;
        }
    }
}
