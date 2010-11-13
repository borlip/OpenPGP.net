using System;
using OpenPGP.Core;

namespace OpenPGP
{
    /// <summary>
    /// Provides helper functions to the <see cref="Simple" /> class.
    /// </summary>
    [CLSCompliant(false)]
    public interface ISimpleDecryptionHelper
    {
        string GetSymmetricDecryptionPassphrase();
        string GetPassphraseForPrivateKey(KeyIdentifier keyIdentifier);
    }
}
