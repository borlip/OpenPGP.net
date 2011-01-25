namespace OpenPGP.Core
{
    /// <summary>
    /// Packet types as specified in RFC 4880 at http://tools.ietf.org/html/rfc4880#section-4.3
    /// </summary>
    public static class PacketTypes
    {
        public const int Reserved = 0;
        public const int PublicKeyEncryptedSessionKey = 1;
        public const int Signature = 2;
        public const int SymmetricKeyEncryptedSessionKey = 3;
        public const int OnePassSignature = 4;
        public const int SecretKey = 5;
        public const int PublicKey = 6;
        public const int SecretSubkey = 7;
        public const int CompressedData = 8;
        public const int SymmetricallyEncryptedData = 9;
        public const int Marker = 10;
        public const int LiteralData = 11;
        public const int Trust = 12;
        public const int UserId = 13;
        public const int PublicSubkey = 14;
        public const int UserAttribute = 17;
        public const int SymmetricallyEncryptedAndIntegrityProtectedData = 18;
        public const int ModificationDetectionCode = 19;
    }
}
