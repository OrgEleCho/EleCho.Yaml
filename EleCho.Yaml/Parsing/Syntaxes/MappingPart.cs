using System;
using System.Collections.Generic;
using EleCho.Compiling;

namespace EleCho.Yaml.Parsing.Syntaxes
{
    public class MappingPart : Syntax
    {
        private readonly List<MappingItem> _items = new();

        public int Indent => _items[0].Indent;
        public ReadOnlyMemory<char> IndentText => _items[0].IndentText;

        public IEnumerable<MappingItem> Items => _items.AsReadOnly();

        public MappingPart(MappingItem firstItem)
            : base(firstItem)
        {
            _items.Add(firstItem);
        }

        public void AddItem(MappingItem blockMappingItem)
        {
            _items.Add(blockMappingItem);
            ExpandTextRange(blockMappingItem);
        }
    }
}
