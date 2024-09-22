using System;
using System.Collections.Generic;
using System.Linq;

namespace EleCho.Compiling
{
    public abstract class Grammar<TInput, TResult> : IGrammar
        where TInput : ISyntax
        where TResult : ISyntax
    {
        int IGrammar.RequireCount => 1;

        public abstract bool CanConstruct(GrammarContext context, TInput input);
        public abstract IEnumerable<TResult> Construct(GrammarContext context, TInput input);
        bool IGrammar.CanConstruct(GrammarContext context, ReadOnlySpan<ISyntax> syntaxes)
        {
            if (syntaxes.Length != 1)
            {
                return false;
            }

            if (syntaxes[0] is not TInput input1 ||
                !CanConstruct(context, input1))
            {
                return false;
            }

            return true;
        }

        IEnumerable<ISyntax> IGrammar.Construct(GrammarContext context, ReadOnlyMemory<ISyntax> syntaxes)
        {
            var result = Construct(context, (TInput)syntaxes.Span[0]);

            if (result is null)
            {
                yield break;
            }

            foreach (var syntax in result)
            {
                yield return syntax;
            }
        }
    }

    public abstract class Grammar<TInput1, TInput2, TResult> : IGrammar
        where TInput1 : ISyntax
        where TInput2 : ISyntax
        where TResult : ISyntax
    {
        int IGrammar.RequireCount => 2;

        public abstract bool CanConstruct(GrammarContext context, TInput1 input1, TInput2 input2);
        public abstract IEnumerable<TResult> Construct(GrammarContext context, TInput1 input1, TInput2 input2);
        bool IGrammar.CanConstruct(GrammarContext context, ReadOnlySpan<ISyntax> syntaxes)
        {
            if (syntaxes.Length != 2)
            {
                return false;
            }

            if (syntaxes[0] is not TInput1 input1 ||
                syntaxes[1] is not TInput2 input2 ||
                !CanConstruct(context, input1, input2))
            {
                return false;
            }

            return true;
        }

        IEnumerable<ISyntax> IGrammar.Construct(GrammarContext context, ReadOnlyMemory<ISyntax> syntaxes)
        {
            var result = Construct(context,
                (TInput1)syntaxes.Span[0],
                (TInput2)syntaxes.Span[1]);

            if (result is null)
            {
                yield break;
            }

            foreach (var syntax in result)
            {
                yield return syntax;
            }
        }
    }

    public abstract class Grammar<TInput1, TInput2, TInput3, TResult> : IGrammar
        where TInput1 : ISyntax
        where TInput2 : ISyntax
        where TInput3 : ISyntax
        where TResult : ISyntax
    {
        int IGrammar.RequireCount => 3;

        public abstract bool CanConstruct(GrammarContext context, TInput1 input1, TInput2 input2, TInput3 input3);
        public abstract IEnumerable<TResult> Construct(GrammarContext context, TInput1 input1, TInput2 input2, TInput3 input3);
        bool IGrammar.CanConstruct(GrammarContext context, ReadOnlySpan<ISyntax> syntaxes)
        {
            if (syntaxes.Length != 3)
            {
                return false;
            }

            if (syntaxes[0] is not TInput1 input1 ||
                syntaxes[1] is not TInput2 input2 ||
                syntaxes[2] is not TInput3 input3 ||
                !CanConstruct(context, input1, input2, input3))
            {
                return false;
            }

            return true;
        }

        IEnumerable<ISyntax> IGrammar.Construct(GrammarContext context, ReadOnlyMemory<ISyntax> syntaxes)
        {
            var result = Construct(context,
                (TInput1)syntaxes.Span[0],
                (TInput2)syntaxes.Span[1],
                (TInput3)syntaxes.Span[2]);

            if (result is null)
            {
                yield break;
            }

            foreach (var syntax in result)
            {
                yield return syntax;
            }
        }
    }

    public abstract class Grammar<TInput1, TInput2, TInput3, TInput4, TResult> : IGrammar
        where TInput1 : ISyntax
        where TInput2 : ISyntax
        where TInput3 : ISyntax
        where TInput4 : ISyntax
        where TResult : ISyntax
    {
        int IGrammar.RequireCount => 4;

        public abstract bool CanConstruct(GrammarContext context, TInput1 input1, TInput2 input2, TInput3 input3, TInput4 input4);
        public abstract IEnumerable<TResult> Construct(GrammarContext context, TInput1 input1, TInput2 input2, TInput3 input3, TInput4 input4);
        bool IGrammar.CanConstruct(GrammarContext context, ReadOnlySpan<ISyntax> syntaxes)
        {
            if (syntaxes.Length != 4)
            {
                return false;
            }

            if (syntaxes[0] is not TInput1 input1 ||
                syntaxes[1] is not TInput2 input2 ||
                syntaxes[2] is not TInput3 input3 ||
                syntaxes[3] is not TInput4 input4 ||
                !CanConstruct(context, input1, input2, input3, input4))
            {
                return false;
            }

            return true;
        }

        IEnumerable<ISyntax> IGrammar.Construct(GrammarContext context, ReadOnlyMemory<ISyntax> syntaxes)
        {
            var result = Construct(context,
                (TInput1)syntaxes.Span[0],
                (TInput2)syntaxes.Span[1],
                (TInput3)syntaxes.Span[2],
                (TInput4)syntaxes.Span[3]);

            if (result is null)
            {
                yield break;
            }

            foreach (var syntax in result)
            {
                yield return syntax;
            }
        }
    }

    public abstract class Grammar<TInput1, TInput2, TInput3, TInput4, TInput5, TResult> : IGrammar
        where TInput1 : ISyntax
        where TInput2 : ISyntax
        where TInput3 : ISyntax
        where TInput4 : ISyntax
        where TInput5 : ISyntax
        where TResult : ISyntax
    {
        int IGrammar.RequireCount => 5;

        public abstract bool CanConstruct(GrammarContext context, TInput1 input1, TInput2 input2, TInput3 input3, TInput4 input4, TInput5 input5);
        public abstract IEnumerable<TResult> Construct(GrammarContext context, TInput1 input1, TInput2 input2, TInput3 input3, TInput4 input4, TInput5 input5);
        bool IGrammar.CanConstruct(GrammarContext context, ReadOnlySpan<ISyntax> syntaxes)
        {
            if (syntaxes.Length != 5)
            {
                return false;
            }

            if (syntaxes[0] is not TInput1 input1 ||
                syntaxes[1] is not TInput2 input2 ||
                syntaxes[2] is not TInput3 input3 ||
                syntaxes[3] is not TInput4 input4 ||
                syntaxes[4] is not TInput5 input5 ||
                !CanConstruct(context, input1, input2, input3, input4, input5))
            {
                return false;
            }

            return true;
        }

        IEnumerable<ISyntax> IGrammar.Construct(GrammarContext context, ReadOnlyMemory<ISyntax> syntaxes)
        {
            var result = Construct(context,
                (TInput1)syntaxes.Span[0],
                (TInput2)syntaxes.Span[1],
                (TInput3)syntaxes.Span[2],
                (TInput4)syntaxes.Span[3],
                (TInput5)syntaxes.Span[4]);

            if (result is null)
            {
                yield break;
            }

            foreach (var syntax in result)
            {
                yield return syntax;
            }
        }
    }

    public abstract class Grammar<TInput1, TInput2, TInput3, TInput4, TInput5, TInput6, TResult> : IGrammar
        where TInput1 : ISyntax
        where TInput2 : ISyntax
        where TInput3 : ISyntax
        where TInput4 : ISyntax
        where TInput5 : ISyntax
        where TInput6 : ISyntax
        where TResult : ISyntax
    {
        int IGrammar.RequireCount => 6;

        public abstract bool CanConstruct(GrammarContext context, TInput1 input1, TInput2 input2, TInput3 input3, TInput4 input4, TInput5 input5, TInput6 input6);
        public abstract IEnumerable<TResult> Construct(GrammarContext context, TInput1 input1, TInput2 input2, TInput3 input3, TInput4 input4, TInput5 input5, TInput6 input6);

        bool IGrammar.CanConstruct(GrammarContext context, ReadOnlySpan<ISyntax> syntaxes)
        {
            if (syntaxes.Length != 6)
            {
                return false;
            }

            if (syntaxes[0] is not TInput1 input1 ||
                syntaxes[1] is not TInput2 input2 ||
                syntaxes[2] is not TInput3 input3 ||
                syntaxes[3] is not TInput4 input4 ||
                syntaxes[4] is not TInput5 input5 ||
                syntaxes[5] is not TInput6 input6 ||
                !CanConstruct(context, input1, input2, input3, input4, input5, input6))
            {
                return false;
            }

            return true;
        }

        IEnumerable<ISyntax> IGrammar.Construct(GrammarContext context, ReadOnlyMemory<ISyntax> syntaxes)
        {
            var result = Construct(context,
                (TInput1)syntaxes.Span[0],
                (TInput2)syntaxes.Span[1],
                (TInput3)syntaxes.Span[2],
                (TInput4)syntaxes.Span[3],
                (TInput5)syntaxes.Span[4],
                (TInput6)syntaxes.Span[5]);

            if (result is null)
            {
                yield break;
            }

            foreach (var syntax in result)
            {
                yield return syntax;
            }
        }
    }
}
