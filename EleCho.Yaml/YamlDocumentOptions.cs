namespace EleCho.Yaml
{
    public record struct YamlDocumentOptions
    {
        public bool AllowTrailingCommas { get; set; }

        public bool AllowHorizontalTab { get; set; }

        public YamlCommentHandling CommentHandling { get; set; }

        public int MaxDepth { get; set; }
    }
}
