namespace OpenPGP
{
    /// <summary>
    /// The values inside an ASCII armor header.
    /// </summary>
    public class AsciiArmorHeader
    {
        /// <summary>
        /// Gets or sets the type of the content.
        /// </summary>
        /// <value>The type of the content.</value>
        public PGPContentType ContentType { get; private set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>The version.</value>
        public string Version { get; private set; }

        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        /// <value>The comment.</value>
        public string Comment { get; private set; }

        /// <summary>
        /// Gets or sets the character set (e.g., UTF-8).
        /// </summary>
        /// <value>The character set.</value>
        public string Charset { get; private set; }

        /// <summary>
        /// Gets or sets the message ID.
        /// </summary>
        /// <value>The message ID.</value>
        public string MessageId { get; private set; }

        /// <summary>
        /// Gets or sets the hash algorithm(s) used in a cleartext signed message.
        /// </summary>
        /// <value>The hash.</value>
        public string Hash { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AsciiArmorHeader"/> class.
        /// </summary>
        /// <param name="contentType">Type of the content.</param>
        /// <param name="version">The version.</param>
        /// <param name="comment">The comment.</param>
        /// <param name="charset">The character set.</param>
        /// <param name="messageId">The message ID.</param>
        /// <param name="hash">The hash.</param>
        public AsciiArmorHeader(PGPContentType contentType, string version, string comment, string charset, string messageId, string hash)
        {
            ContentType = contentType;
            Version = version;
            Comment = comment;
            Charset = charset;
            MessageId = messageId;
            Hash = hash;
        }
    }
}
