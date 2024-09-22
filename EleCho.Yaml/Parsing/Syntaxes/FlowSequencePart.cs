using System.Collections.Generic;
using EleCho.Compiling;

namespace EleCho.Yaml.Parsing.Syntaxes
{
    public class FlowSequencePart : Syntax
    {
        private readonly List<FlowSequenceItem> _items = new();

        public IEnumerable<FlowSequenceItem> Items => _items.AsReadOnly();

        public FlowSequencePart(FlowSequenceItem firstItem)
            : base(firstItem)
        {
            _items.Add(firstItem);
        }

        public void AddItem(FlowSequenceItem flowSequenceItem)
        {
            _items.Add(flowSequenceItem);
            ExpandTextRange(flowSequenceItem);
        }
    }
}
