using System;
using System.Collections.Generic;
using System.Text;

namespace EleCho.Yaml.Parsing
{
    public static class YamlCharacters
    {
        public static bool IsSequenceEntry(char c) => c == '-';
        public static bool IsMappingKey(char c) => c == '?';
        public static bool IsMappingValue(char c) => c == ':';
    }
}
