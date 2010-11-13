using System;
using OpenPGP.Core;

namespace OpenPGP
{
    /// <summary>
    /// The result of a decryption operation.
    /// </summary>
    public class DecryptResult
    {
        public bool IsSigned { get; set; }
        public bool IsSignatureGood { get; private set; }

        [CLSCompliant(false)]
        public KeyIdentifier SigningKeyIdentifier { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DecryptResult"/> class.
        /// </summary>
        /// <param name="isSigned">if set to <c>true</c>, the source data is signed.</param>
        /// <param name="isSignatureGood">if set to <c>true</c>, the signature is good.</param>
        /// <param name="signingKeyIdentifier">The signing key identifier.</param>
        [CLSCompliant(false)]
        public DecryptResult(bool isSigned, bool isSignatureGood, KeyIdentifier signingKeyIdentifier)
        {
            IsSigned = isSigned;
            IsSignatureGood = isSignatureGood;
            SigningKeyIdentifier = signingKeyIdentifier;
        }
    }
}
