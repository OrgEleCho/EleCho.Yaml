using System;
using System.Collections.Generic;
using System.Text;
using EleCho.Compiling;

namespace EleCho.Yaml.Parsing
{
    public static class YamlCharacters
    {
        public static bool IsPrintable(char c) =>
            c == '\t'
            || c == '\n'
            || c == '\r'
            || (c >= '\x20' && c <= '\x7E')
            || c == '\x85'
            || (c >= '\xA0' && c <= '\xD7FF')
            || (c >= '\xE000' && c <= '\xFFFD')
            //|| (c >= '\x010000' && c <= '\x10FFFF')
            ;


        public static bool IsEscape(char c) => c == '\\';

        #region Indicators

        public static bool IsSequenceEntry(char c) => c == '-';
        public static bool IsMappingKey(char c) => c == '?';
        public static bool IsMappingValue(char c) => c == ':';

        public static bool IsCollectEntry(char c) => c == ',';
        public static bool IsSequenceStart(char c) => c == '[';
        public static bool IsSequenceEnd(char c) => c == ']';
        public static bool IsMappingStart(char c) => c == '{';
        public static bool IsMappingEnd(char c) => c == '}';


        public static bool IsComment(char c) => c == '#';
        public static bool IsAnchor(char c) => c == '&';
        public static bool IsAlias(char c) => c == '*';
        public static bool IsTag(char c) => c == '!';


        public static bool IsLiteral(char c) => c == '|';
        public static bool IsFolded(char c) => c == '>';

        public static bool IsSingleQuote(char c) => c == '\'';
        public static bool IsDoubleQuote(char c) => c == '"';

        public static bool IsIndicator(char c) =>
            IsSequenceEntry(c)
            || IsMappingKey(c)
            || IsMappingValue(c)
            || IsCollectEntry(c)
            || IsSequenceStart(c)
            || IsSequenceEnd(c)
            || IsMappingStart(c)
            || IsMappingEnd(c)
            || IsComment(c)
            || IsAnchor(c)
            || IsAlias(c)
            || IsTag(c)
            || IsLiteral(c)
            || IsFolded(c)
            || IsSingleQuote(c)
            || IsDoubleQuote(c);

        public static bool IsNonIndicator(char c) => !IsIndicator(c);

        #endregion


        #region Line Break

        public static bool IsLineFeed(char c) => c == '\n';
        public static bool IsCarriageReturn(char c) => c == '\r';

        public static bool IsLineBreak(char c) =>
            IsLineFeed(c)
            || IsCarriageReturn(c);

        public static bool IsNonLineBreak(char c) => !IsLineBreak(c);

        #endregion


        #region Whtie Space

        public static bool IsSpace(char c) => c == ' ';
        public static bool IsTab(char c) => c == '\t';
        public static bool IsWhiteSpace(char c) =>
            IsSpace(c)
            || IsTab(c);

        public static bool IsNonWhiteSpace(char c) => !IsWhiteSpace(c);

        #endregion


        #region Miscellaneous

        public static bool IsDecDigit(char c) => c >= '0' && c <= '9';
        public static bool IsHexDigit(char c) =>
            c >= '0' && c <= '9'
            || c >= 'A' && c <= 'F'
            || c >= 'a' && c <= 'f';
        public static bool IsAsciiLetter(char c) =>
            c >= 'A' && c <= 'Z'
            || c >= 'a' && c <= 'z';

        public static bool IsWord(char c) =>
            IsDecDigit(c)
            || IsAsciiLetter(c)
            || c == '-';

        //public static bool IsUriChar

        #endregion


        #region Parsing

        public static bool IsCommonScalarStart(char c) =>
            IsPrintable(c)
            && IsNonLineBreak(c)
            && IsNonIndicator(c);

        public static bool IsCommonScalarContent(char c) =>
            IsPrintable(c)
            && IsNonLineBreak(c)
            && !IsMappingValue(c);

        #endregion
    }
}
