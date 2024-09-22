using System;
using System.Collections.Generic;
using EleCho.Compiling;

namespace EleCho.Yaml.Parsing.Syntaxes
{
    public class FlowSequence : Syntax
    {
        private readonly List<FlowSequenceItem> _items = new();

        public int Indent => Position;
        public ReadOnlyMemory<char> IndentText => TextSource.Slice(TextStart - Indent, Indent);

        public IEnumerable<FlowSequenceItem> Items => _items.AsReadOnly();

        public FlowSequence(FlowSequenceStart start, FlowSequenceEnd end)
            : base(start, end)
        {

        }

        public FlowSequence(FlowSequenceStart start, FlowSequencePart part, FlowSequenceEnd end)
            : base(start, part, end)
        {
            _items.AddRange(part.Items);
        }

        public void AddItem(FlowSequenceItem blockSequenceItem)
        {
            _items.Add(blockSequenceItem);
            ExpandTextRange(blockSequenceItem);
        }
    }
}
