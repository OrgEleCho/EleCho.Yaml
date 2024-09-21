using System.Collections;
using System.Collections.Generic;

namespace EleCho.Yaml.Nodes
{
    public sealed class YamlArray : YamlNode, IList<YamlNode>
    {
        private readonly IList<YamlNode> _data;

        public int Count => _data.Count;

        public bool IsReadOnly => _data.IsReadOnly;

        public YamlNode this[int index] { get => _data[index]; set => _data[index] = value; }

        public YamlArray(IList<YamlNode> data)
        {
            _data = data;
        }

        public YamlArray() : this(new List<YamlNode>())
        {

        }

        public int IndexOf(YamlNode item) => _data.IndexOf(item);
        public void Insert(int index, YamlNode item) => _data.Insert(index, item);
        public void RemoveAt(int index) => _data.RemoveAt(index);
        public void Add(YamlNode item) => _data.Add(item);
        public void Clear() => _data.Clear();
        public bool Contains(YamlNode item) => _data.Contains(item);
        public void CopyTo(YamlNode[] array, int arrayIndex) => _data.CopyTo(array, arrayIndex);
        public bool Remove(YamlNode item) => _data.Remove(item);
        public IEnumerator<YamlNode> GetEnumerator() => _data.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_data).GetEnumerator();
    }
}
