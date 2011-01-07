using System.Text.RegularExpressions;

namespace OpenPGP.Core
{
    /// <summary>
    /// ASCII armor constants as specified in RFC 4880 at http://tools.ietf.org/html/rfc4880#section-6.2
    /// </summary>
    public static class AsciiArmorConstants
    {
        public const string ArmorLinePrefix = "-----";
        public const string ArmorLineSuffix = "-----";
        public const string ArmorDetectionString1 = ArmorLinePrefix + "BEGIN PGP ";
        public const string ArmorDetectionString2 = ArmorLinePrefix + "END PGP ";
        public const string MessageHeaderLine = ArmorLinePrefix + "BEGIN PGP MESSAGE" + ArmorLineSuffix;
        public const string MessageTailLine = ArmorLinePrefix + "END PGP MESSAGE" + ArmorLineSuffix;
        public const string PublicKeyBlockHeaderLine = ArmorLinePrefix + "BEGIN PGP PUBLIC KEY BLOCK" + ArmorLineSuffix;
        public const string PublicKeyBlockTailLine = ArmorLinePrefix + "END PGP PUBLIC KEY BLOCK" + ArmorLineSuffix;
        public const string PrivateKeyBlockHeaderLine = ArmorLinePrefix + "BEGIN PGP PRIVATE KEY BLOCK" + ArmorLineSuffix;
        public const string PrivateKeyBlockTailLine = ArmorLinePrefix + "END PGP PRIVATE KEY BLOCK" + ArmorLineSuffix;
        public const string SignatureHeaderLine = ArmorLinePrefix + "BEGIN PGP SIGNATURE" + ArmorLineSuffix;
        public const string SignatureTailLine = ArmorLinePrefix + "END PGP SIGNATURE" + ArmorLineSuffix;
        public const string MultipartMessageHeaderLinePattern = ArmorLinePrefix + @"BEGIN PGP MESSAGE, PART (?<partnum>\d+)/(?<numparts>\d+)" + ArmorLineSuffix;
        public const string MultipartMessageTailLinePattern = ArmorLinePrefix + @"END PGP MESSAGE, PART (?<partnum>\d+)/(?<numparts>\d+)" + ArmorLineSuffix;
        public const string MultipartMessageHeaderLineUnspecifiedPattern = ArmorLinePrefix + @"BEGIN PGP MESSAGE, PART (?<partnum>\d+)" + ArmorLineSuffix;
        public const string MultipartMessageTailLineUnspecifiedPattern = ArmorLinePrefix + @"END PGP MESSAGE, PART (?<partnum>\d+)" + ArmorLineSuffix;
        public const string VersionHeader = "Version";
        public const string CommentHeader = "Comment";
        public const string MessageIdHeader = "MessageID";
        public const string HashHeader = "Hash";
        public const string CharsetHeader = "Charset";

        private static readonly Regex _MultipartMessageHeaderLineRegex = new Regex(MultipartMessageHeaderLinePattern);
        public static Regex MultipartMessageHeaderLineRegex
        {
            get { return _MultipartMessageHeaderLineRegex; }
        }

        private static readonly Regex _MultipartMessageTailLineRegex = new Regex(MultipartMessageTailLinePattern);
        public static Regex MultipartMessageTailLineRegex
        {
            get { return _MultipartMessageTailLineRegex; }
        }

        private static readonly Regex _MultipartMessageHeaderLineUnspecifiedRegex = new Regex(MultipartMessageHeaderLineUnspecifiedPattern);
        public static Regex MultipartMessageHeaderLineUnspecifiedRegex
        {
            get { return _MultipartMessageHeaderLineUnspecifiedRegex; }
        }

        private static readonly Regex _MultipartMessageTailLineUnspecifiedRegex = new Regex(MultipartMessageTailLineUnspecifiedPattern);
        public static Regex MultipartMessageTailLineUnspecifiedRegex
        {
            get { return _MultipartMessageTailLineUnspecifiedRegex; }
        }
    }
}
