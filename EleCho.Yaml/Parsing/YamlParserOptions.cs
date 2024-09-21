namespace EleCho.Yaml.Parsing
{
    internal record struct YamlParserOptions
    {
        public bool AllowTrailingCommas { get; set; }

        public bool AllowHorizontalTab { get; set; }

        public YamlCommentHandling CommentHandling { get; set; }

        public int MaxDepth { get; set; }
    }
}