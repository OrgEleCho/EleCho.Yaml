using System;
using System.Collections.Generic;

namespace EleCho.Compiling
{
    public interface IGrammar
    {
        public int RequireCount { get; }

        public bool CanConstruct(GrammarContext context, ReadOnlySpan<ISyntax> syntaxes);
        public IEnumerable<ISyntax> Construct(GrammarContext context, ReadOnlyMemory<ISyntax> syntaxes);
    }
}
