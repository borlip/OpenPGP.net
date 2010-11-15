using System;

namespace OpenPGP
{
    /// <summary>
    /// Provides secret data to a decryption operation.
    /// </summary>
    [CLSCompliant(false)]
    public interface IDecryptionSecretDataProvider
    {
        /// <summary>
        /// Gets the passphrase to decrypt symmetrically encrypted data.
        /// </summary>
        /// <returns>The passphrase to decrypt symmetrically encrypted data.</returns>
        string GetSymmetricDecryptionPassphrase();

        /// <summary>
        /// Gets the passphrase for a private key.
        /// </summary>
        /// <param name="keyIdentifier">The key identifier.</param>
        /// <returns>The passphrase for the specified private key.</returns>
        string GetPassphraseForPrivateKey(KeyIdentifier keyIdentifier);
    }
}
