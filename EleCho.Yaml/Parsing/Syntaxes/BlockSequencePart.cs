using System;
using System.Collections.Generic;
using EleCho.Compiling;

namespace EleCho.Yaml.Parsing.Syntaxes
{
    public class BlockSequencePart : Syntax
    {
        private readonly List<BlockSequenceItem> _items = new();

        public int Indent => _items[0].Indent;
        public ReadOnlyMemory<char> IndentText => _items[0].IndentText;


        public IEnumerable<BlockSequenceItem> Items => _items.AsReadOnly();

        public BlockSequencePart(BlockSequenceItem firstItem)
            : base(firstItem)
        {
            _items.Add(firstItem);
        }

        public void AddItem(BlockSequenceItem blockSequenceItem)
        {
            _items.Add(blockSequenceItem);
            ExpandTextRange(blockSequenceItem);
        }
    }
}
