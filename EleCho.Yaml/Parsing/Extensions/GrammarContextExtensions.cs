using System;
using System.Collections.Generic;
using System.Text;
using EleCho.Compiling;
using EleCho.Yaml.Parsing.Syntaxes;

namespace EleCho.Yaml.Parsing.Extensions
{
    internal static class GrammarContextExtensions
    {
        public static bool IsInFlow(this GrammarContext context)
        {
            if (context.SyntaxCountBefore == 0)
            {
                return false;
            }

            var syntaxBefore = context.GetSyntaxBefore(0);
            return syntaxBefore is FlowMappingStart or FlowSequenceStart;
        }
    }
}
