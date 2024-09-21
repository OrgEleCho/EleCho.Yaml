namespace EleCho.Yaml.Parsing
{
    internal enum YamlTokenKind
    {
        /// <summary>
        /// No token.
        /// </summary>
        None,        // no token
        /// <summary>
        /// Comment
        /// </summary>
        Comment,     // # some comment
        /// <summary>
        /// Start of YAML object
        /// </summary>
        ObjectStart, // {
        /// <summary>
        /// End of YAML object
        /// </summary>
        ObjectEnd,   // }
        /// <summary>
        /// Start of YAML array
        /// </summary>
        ArrayStart,  // [
        /// <summary>
        /// End of YAML array
        /// </summary>
        ArrayEnd,    // ]
        /// <summary>
        /// :
        /// </summary>
        Colon,       // :
        /// <summary>
        /// ,
        /// </summary>
        Comma,       // ,
        /// <summary>
        /// -
        /// </summary>
        Dash,        // -
        /// <summary>
        /// YAML string
        /// </summary>
        String,      // "..."
        /// <summary>
        /// YAML number
        /// </summary>
        Literal,     // some-words, true, false, 3.1415926
        /// <summary>
        /// YAML Anchor
        /// </summary>
        Anchor,
        /// <summary>
        /// YAML Reference
        /// </summary>
        Reference,
    }
}
