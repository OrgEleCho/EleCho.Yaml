using System;
using System.Runtime.Serialization;

namespace EleCho.Yaml
{
    public class YamlException : Exception
    {
        public YamlException()
        {
        }

        public YamlException(string? message) : base(message)
        {
        }

        public YamlException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
