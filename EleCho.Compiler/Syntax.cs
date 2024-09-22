using System;
using System.Diagnostics.SymbolStore;
using System.Linq;

namespace EleCho.Compiling
{
    public class Syntax : ISyntax
    {
        public ReadOnlyMemory<char> TextSource { get; private set; }
        public int TextStart { get; private set; }
        public int TextEnd { get; private set; }
        public int TextLength => TextEnd - TextStart;
        public ReadOnlyMemory<char> Text => TextSource.Slice(TextStart, TextLength);

        public int LineNumber { get; private set; }
        public int Position { get; private set; }
        public int EndPosition { get; private set; }

        public Syntax(ISyntax syntax)
        {
            TextSource = syntax.TextSource;
            TextStart = syntax.TextStart;
            TextEnd = syntax.TextEnd;

            LineNumber = syntax.LineNumber;
            Position = syntax.Position;
            EndPosition = syntax.EndPosition;
        }

        public Syntax(ISyntax syntax, params ISyntax[] additionalContents)
            : this(syntax)
        {
            ExpandTextRange(additionalContents);
        }

        public void ExpandTextRange(int start, int end, int lineNumber, int position, int endPosition)
        {
            if (start < TextStart)
            {
                TextStart = start;
                LineNumber = lineNumber;
                Position = position;
            }
            
            if (end > TextEnd)
            {
                TextEnd = end;
                EndPosition = endPosition;
            }
        }

        public void ExpandTextRange(ISyntax additionalContent)
        {
            if (additionalContent is null ||
                !additionalContent.TextSource.Equals(TextSource))
            {
                throw new ArgumentNullException(nameof(additionalContent));
            }

            ExpandTextRange(additionalContent.TextStart, additionalContent.TextEnd, additionalContent.LineNumber, additionalContent.Position, additionalContent.EndPosition);
        }

        public void ExpandTextRange(params ISyntax[] additionalContents)
        {
            if (additionalContents is null)
            {
                throw new ArgumentNullException(nameof(additionalContents));
            }

            foreach (var additionalContent in additionalContents)
            {
                ExpandTextRange(additionalContent);
            }
        }

        public override string ToString()
        {
            return $"{GetType()} {TextSource.Slice(TextStart, TextLength)}";
        }
    }
}
