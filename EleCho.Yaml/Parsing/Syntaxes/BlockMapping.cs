using System;
using System.Collections.Generic;
using System.Linq;
using EleCho.Compiling;

namespace EleCho.Yaml.Parsing.Syntaxes
{
    public class BlockMapping : Syntax
    {
        private readonly List<MappingItem> _items = new();

        public int Indent => _items[0].Indent;
        public ReadOnlyMemory<char> IndentText => _items[0].IndentText;

        public IEnumerable<MappingItem> Items => _items.AsReadOnly();

        public BlockMapping(MappingPart part)
            : base(part)
        {
            _items.AddRange(part.Items);
        }

        public void AddItem(MappingItem blockMappingItem)
        {
            _items.Add(blockMappingItem);
            ExpandTextRange(blockMappingItem);
        }
    }
}
