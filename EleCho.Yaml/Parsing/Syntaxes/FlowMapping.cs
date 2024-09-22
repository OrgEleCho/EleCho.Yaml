using System;
using System.Collections.Generic;
using EleCho.Compiling;

namespace EleCho.Yaml.Parsing.Syntaxes
{
    public class FlowMapping : Syntax
    {
        private readonly List<MappingItem> _items = new();

        public int Indent => Position;
        public ReadOnlyMemory<char> IndentText => TextSource.Slice(TextStart - Indent, Indent);

        public IEnumerable<MappingItem> Items => _items.AsReadOnly();

        public FlowMapping(FlowMappingStart start, FlowMappingEnd end)
            : base(start, end)
        {

        }

        public FlowMapping(FlowMappingStart start, MappingPart part, FlowMappingEnd end)
            : base(start, part, end)
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
