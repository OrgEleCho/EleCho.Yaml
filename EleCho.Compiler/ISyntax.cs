using System;

namespace EleCho.Compiler
{
    public interface ISyntax
    {
        public ReadOnlyMemory<char> TextSource { get; }
        public int TextStart { get; }
        public int TextEnd { get; }
    }
}
