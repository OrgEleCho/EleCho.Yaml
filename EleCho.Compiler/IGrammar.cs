using System;

namespace EleCho.Compiler
{
    public interface IGrammar
    {
        public int RequireCount { get; }
        public Type TargetType { get; }

        public bool CanConstruct(ReadOnlySpan<ISyntax> syntaxes);
        public ISyntax Construct(ReadOnlySpan<ISyntax> syntaxes);
    }
}
