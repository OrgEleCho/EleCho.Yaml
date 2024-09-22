using System;
using System.Collections;
using System.Collections.Generic;
using EleCho.Compiling;

namespace EleCho.Yaml.Parsing.Syntaxes
{
    public class MappingItem : Syntax
    {
        public int Indent => Key.Position;
        public ReadOnlyMemory<char> IndentText => TextSource.Slice(TextStart - Indent, Indent);

        public MappingItem(MappingKey key, ISyntax value)
            : base(key, value)
        {
            Key = key;
            Value = value;
        }

        public MappingKey Key { get; }
        public ISyntax Value { get; }
    }
}
