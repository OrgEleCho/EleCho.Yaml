using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EleCho.Compiling
{
    public sealed record class CharSequence : ISyntax
    {
        public ReadOnlyMemory<char> TextSource { get; }
        public int TextStart { get; }
        public int TextEnd { get; set; }
        public int TextLength => TextEnd - TextStart;
        public ReadOnlyMemory<char> Text => TextSource.Slice(TextStart, TextLength);

        public int LineNumber { get; }
        public int Position { get; }
        public int EndPosition { get; }

        public CharSequence(ReadOnlyMemory<char> textSource, int textStart, int textEnd, int lineNumber, int position, int endPosition)
        {
            TextSource = textSource;
            TextStart = textStart;
            TextEnd = textEnd;

            LineNumber = lineNumber;
            Position = position;
            EndPosition = endPosition;
        }

        public override string ToString()
        {
            return $"CharSequence {Text}";
        }
    }
}
