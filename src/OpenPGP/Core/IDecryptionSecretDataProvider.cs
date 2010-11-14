using System;

namespace OpenPGP.Core
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
