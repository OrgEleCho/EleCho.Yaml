using System;

namespace EleCho.Compiling
{
    public interface ISyntax
    {
        public ReadOnlyMemory<char> TextSource { get; }
        public int TextStart { get; }
        public int TextEnd { get; }

        public int LineNumber { get; }
        public int Position { get; }
        public int EndPosition { get; }
    }
}
