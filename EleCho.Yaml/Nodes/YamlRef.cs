using System;

namespace EleCho.Yaml.Nodes
{
    public sealed class YamlRef : YamlNode
    {
        public string? Target { get; set; }

        public YamlNode? Resolve(YamlObject rootObject)
        {
            if (rootObject is null)
            {
                throw new ArgumentNullException(nameof(rootObject));
            }

            if (string.IsNullOrWhiteSpace(Target))
            {
                return null;
            }

            if (rootObject.AnchorName == Target)
            {
                return rootObject;
            }

            foreach (var value in rootObject.Values)
            {
                if (value is not YamlObject subObject)
                {
                    continue;
                }

                if (Resolve(subObject) is YamlNode resultNode)
                {
                    return resultNode;
                }
            }

            return null;
        }
    }
}
