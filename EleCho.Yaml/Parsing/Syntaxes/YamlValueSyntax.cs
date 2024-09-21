using System;
using System.Collections.Generic;
using System.Text;
using EleCho.Compiler;

namespace EleCho.Yaml.Parsing.Syntaxes
{
    public class YamlValue
    {
        public YamlValue(ReadOnlyMemory<char> textSource, int textStart, int textEnd)
        {

        }

        public static bool IsStartChar(char c)
        {
            return 
                char.IsLetter(c)
        }
    }
}
