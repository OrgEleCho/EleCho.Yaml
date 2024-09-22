using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace EleCho.Yaml
{
    public class YamlException : Exception
    {
        public int Index { get; set; }
        public int LineNumber { get; set; }
        public int Position { get; set; }

        public YamlException()
        {
        }

        public YamlException(string message) : base(message)
        {
        }

        public YamlException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
