using EleCho.Compiling;

namespace EleCho.Yaml.Parsing.Syntaxes
{
    public class MappingKey : Syntax
    {
        private readonly Colon _colon;

        public ISyntax Key { get; }
        public int TailSpacing => TextEnd - _colon.TextStart - 1;

        public MappingKey(ISyntax key, Colon colon)
            : base(key, colon)
        {
            Key = key;
            _colon = colon;
        }
    }
}
