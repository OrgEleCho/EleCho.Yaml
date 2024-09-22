using System;

namespace EleCho.Compiling
{
    public sealed record class EndOfFile : ISyntax
    {
        public ReadOnlyMemory<char> TextSource { get; }
        public int TextStart { get; }
        public int TextEnd { get; }
        public int LineNumber { get; }
        public int Position { get; }
        public int EndPosition { get; }


        public EndOfFile(ReadOnlyMemory<char> textSource, int index, int lineNumber, int position)
        {
            TextSource = textSource;
            TextStart = index;
            TextEnd = index;
            LineNumber = LineNumber;
            Position = position;
            EndPosition = position;
        }

        public override string ToString()
        {
            return nameof(EndOfFile);
        }
    }
}
