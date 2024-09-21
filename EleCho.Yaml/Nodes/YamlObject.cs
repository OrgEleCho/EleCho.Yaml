using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace EleCho.Yaml.Nodes
{
    public sealed class YamlObject : YamlNode, IDictionary<string, YamlNode>
    {
        private readonly IDictionary<string, YamlNode> _data;

        public YamlObject(IDictionary<string, YamlNode> data)
        {
            _data = data;
        }

        public YamlObject() : 
            this(new Dictionary<string, YamlNode>())
        {

        }

        public YamlNode this[string key] { get => ((IDictionary<string, YamlNode>)_data)[key]; set => ((IDictionary<string, YamlNode>)_data)[key] = value; }

        public ICollection<string> Keys => ((IDictionary<string, YamlNode>)_data).Keys;

        public ICollection<YamlNode> Values => ((IDictionary<string, YamlNode>)_data).Values;

        public int Count => ((ICollection<KeyValuePair<string, YamlNode>>)_data).Count;

        public bool IsReadOnly => ((ICollection<KeyValuePair<string, YamlNode>>)_data).IsReadOnly;

        public void Add(string key, YamlNode value) => ((IDictionary<string, YamlNode>)_data).Add(key, value);
        public void Add(KeyValuePair<string, YamlNode> item) => ((ICollection<KeyValuePair<string, YamlNode>>)_data).Add(item);
        public void Clear() => ((ICollection<KeyValuePair<string, YamlNode>>)_data).Clear();
        public bool Contains(KeyValuePair<string, YamlNode> item) => ((ICollection<KeyValuePair<string, YamlNode>>)_data).Contains(item);
        public bool ContainsKey(string key) => ((IDictionary<string, YamlNode>)_data).ContainsKey(key);
        public void CopyTo(KeyValuePair<string, YamlNode>[] array, int arrayIndex) => ((ICollection<KeyValuePair<string, YamlNode>>)_data).CopyTo(array, arrayIndex);
        public IEnumerator<KeyValuePair<string, YamlNode>> GetEnumerator() => ((IEnumerable<KeyValuePair<string, YamlNode>>)_data).GetEnumerator();
        public bool Remove(string key) => ((IDictionary<string, YamlNode>)_data).Remove(key);
        public bool Remove(KeyValuePair<string, YamlNode> item) => ((ICollection<KeyValuePair<string, YamlNode>>)_data).Remove(item);
        public bool TryGetValue(string key, [MaybeNullWhen(false)] out YamlNode? value) => ((IDictionary<string, YamlNode>)_data).TryGetValue(key, out value);
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_data).GetEnumerator();
    }
}
