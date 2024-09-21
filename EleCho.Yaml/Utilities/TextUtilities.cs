using System;
using System.Collections.Generic;
using System.Text;

namespace EleCho.Yaml.Utilities
{
    internal static class TextUtilities
    {
        public static int GetSpaceLength(char c)
        {
            if (c == ' ')
            {
                return 1;
            }
            else if (c == '\t')
            {
                return 4;
            }
            else if (char.IsWhiteSpace(c))
            {
                return 1;
            }
            else
            {
                throw new ArgumentException("Not a whitespace string", nameof(c));
            }
        }

        public static int GetSpaceLength(ReadOnlySpan<char> text)
        {
            int length = 0;
            foreach (var c in text)
            {
                if (c == ' ')
                {
                    length += 1;
                }
                else if (c == '\t')
                {
                    length += 4;
                }
                else if (char.IsWhiteSpace(c))
                {
                    length += 1;
                }
                else
                {
                    throw new ArgumentException("Not a whitespace string", nameof(text));
                }
            }

            return length;
        }
    }
}
