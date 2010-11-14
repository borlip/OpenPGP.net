using System;

namespace OpenPGP
{
    /// <summary>
    /// Provides secret data to a decryption operation.
    /// </summary>
    [CLSCompliant(false)]
    public interface IDecryptionSecretDataProvider
    {
        string GetSymmetricDecryptionPassphrase();
        string GetPassphraseForPrivateKey(KeyIdentifier keyIdentifier);
    }
}
