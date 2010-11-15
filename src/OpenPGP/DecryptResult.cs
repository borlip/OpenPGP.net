using System;

namespace OpenPGP
{
    /// <summary>
    /// The result of a decryption operation.
    /// </summary>
    public class DecryptResult
    {
        /// <summary>
        /// Gets or sets a value indicating whether the operation was successful.
        /// </summary>
        /// <value><c>true</c> if this the operation was successful; otherwise, <c>false</c>.</value>
        public bool IsSuccessful { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether the data is signed.
        /// </summary>
        /// <value><c>true</c> if the data is signed; otherwise, <c>false</c>.</value>
        public bool IsSigned { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether the signature is good.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if the signature is good; otherwise, <c>false</c>.
        /// </value>
        /// <remarks>
        /// Value only has meaning if <see cref="IsSigned" /> is <c>true</c>.
        /// </remarks>
        public bool IsSignatureGood { get; private set; }

        /// <summary>
        /// Gets or sets the signing key identifier.
        /// </summary>
        /// <value>The signing key identifier.</value>
        /// <remarks>
        /// Value only has meaning if <see cref="IsSigned" /> is <c>true</c>.
        /// </remarks>
        [CLSCompliant(false)]
        public KeyIdentifier SigningKeyIdentifier { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DecryptResult"/> class.
        /// </summary>
        /// <param name="isSuccessful">if set to <c>true</c>, the operation was successful.</param>
        /// <param name="isSigned">if set to <c>true</c>, the source data is signed.</param>
        /// <param name="isSignatureGood">if set to <c>true</c>, the signature is good.</param>
        /// <param name="signingKeyIdentifier">The signing key identifier.</param>
        [CLSCompliant(false)]
        public DecryptResult(bool isSuccessful, bool isSigned, bool isSignatureGood, KeyIdentifier signingKeyIdentifier)
        {
            IsSuccessful = isSuccessful;
            IsSigned = isSigned;
            IsSignatureGood = isSignatureGood;
            SigningKeyIdentifier = signingKeyIdentifier;
        }
    }
}
