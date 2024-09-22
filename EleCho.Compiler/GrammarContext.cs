using System;

namespace EleCho.Compiling
{
    public record struct GrammarContext
    {
        private readonly Parser _parser;
        private readonly IGrammar _grammar;

        public Parser Parser => _parser;
        public IGrammar Grammar => _grammar;
        public int SyntaxCountBefore => Parser.Result.Count - Grammar.RequireCount;


        public ISyntax GetSyntaxBefore(int offset)
        {
            if (offset >= SyntaxCountBefore)
            {
                throw new ArgumentOutOfRangeException(nameof(offset));
            }

            return Parser.Result.GetTail(Grammar.RequireCount + offset);
        }

        public ReadOnlyMemory<ISyntax> GetSyntaxesBefore(int count)
        {
            if (count > SyntaxCountBefore)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            return Parser.Result.GetTails(count, Grammar.RequireCount);
        }

        public GrammarContext(Parser parser, IGrammar grammar)
        {
            _parser = parser;
            _grammar = grammar;
        }
    }
}
