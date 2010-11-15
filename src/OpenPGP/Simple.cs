using System;
using System.Collections.Generic;
using System.IO;
using OpenPGP.Core;

namespace OpenPGP
{
    /// <summary>
    /// Simple, common implementations of OpenPGP. Designed to give a "quick start" to using OpenPGP.
    /// </summary>
    public static class Simple
    {
        /// <summary>
        /// Encrypts the specified source file with public key encryption.
        /// </summary>
        /// <param name="sourceFileName">Name of the source file.</param>
        /// <param name="destinationFileName">Name of the destination file.</param>
        /// <param name="destinationArmorType">Type of the destination armor.</param>
        /// <param name="recipients">The recipients.</param>
        /// <param name="publicKeyRingFileName">Name of the public key ring file.</param>
        /// <remarks>
        /// <paramref name="recipients"/> is a list of strings that can contain e-mail addresses or hexadecimal key identifiers (e.g., "0xDEADBEEF")
        /// </remarks>
        public static void Encrypt(
            string sourceFileName,
            string destinationFileName,
            ArmorType destinationArmorType,
            IEnumerable<string> recipients,
            string publicKeyRingFileName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Encrypts the specified source file symmetrically.
        /// </summary>
        /// <param name="sourceFileName">Name of the source file.</param>
        /// <param name="destinationFileName">Name of the destination file.</param>
        /// <param name="destinationArmorType">Type of the destination armor.</param>
        /// <param name="passphrase">The passphrase.</param>
        public static void Encrypt(
            string sourceFileName,
            string destinationFileName,
            ArmorType destinationArmorType,
            string passphrase)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Clearsign the specified source file.
        /// </summary>
        /// <param name="sourceFileName">Name of the source file.</param>
        /// <param name="destinationFileName">Name of the destination file.</param>
        /// <param name="destinationArmorType">Type of the destination armor.</param>
        /// <param name="privateKeyRingFileName">Name of the private key ring file.</param>
        /// <param name="privateKeyIdentifier">The private key identifier.</param>
        /// <param name="privateKeyPassphrase">The private key passphrase.</param>
        public static void Sign(
            string sourceFileName,
            string destinationFileName,
            ArmorType destinationArmorType,
            string privateKeyRingFileName,
            string privateKeyIdentifier,
            string privateKeyPassphrase
            )
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Signs the specified source file with a detached signature.
        /// </summary>
        /// <param name="sourceFileName">Name of the source file.</param>
        /// <param name="signatureFileName">Name of the signature file.</param>
        /// <param name="privateKeyRingFileName">Name of the private key ring file.</param>
        /// <param name="privateKeyIdentifier">The private key identifier.</param>
        /// <param name="privateKeyPassphrase">The private key passphrase.</param>
        public static void SignDetached(
            string sourceFileName,
            string signatureFileName,
            string privateKeyRingFileName,
            string privateKeyIdentifier,
            string privateKeyPassphrase
            )
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Encrypts and signs the specified source file.
        /// </summary>
        /// <param name="sourceFileName">Name of the source file.</param>
        /// <param name="destinationFileName">Name of the destination file.</param>
        /// <param name="destinationArmorType">Type of the destination armor.</param>
        /// <param name="publicKeyRingFileName">Name of the public key ring file.</param>
        /// <param name="recipients">The recipients.</param>
        /// <param name="privateKeyRingFileName">Name of the private key ring file.</param>
        /// <param name="privateKeyIdentifier">The private key identifier.</param>
        /// <param name="privateKeyPassphrase">The private key passphrase.</param>
        /// <remarks>
        /// <paramref name="recipients"/> is a list of strings that can contain e-mail addresses or hexadecimal key identifiers (e.g., "0xDEADBEEF")
        /// </remarks>
        public static void EncryptAndSign(
            string sourceFileName, 
            string destinationFileName,
            ArmorType destinationArmorType,
            string publicKeyRingFileName, 
            IEnumerable<string> recipients, 
            string privateKeyRingFileName, 
            string privateKeyIdentifier, 
            string privateKeyPassphrase)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Decrypts the specified source file.
        /// </summary>
        /// <param name="sourceFileName">Name of the source file.</param>
        /// <param name="destinationFileName">Name of the destination file.</param>
        /// <param name="secretDataProvider">The secret data provider.</param>
        /// <returns>A <see cref="DecryptResult" /> indicating the status of the decryption.</returns>
        [CLSCompliant(false)]
        public static void Decrypt(
            string sourceFileName, 
            string destinationFileName, 
            IDecryptionSecretDataProvider secretDataProvider)
        {
            using (var source = new FileStream(sourceFileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var destination = new FileStream(destinationFileName, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    Decrypt(source, destination, secretDataProvider);
                }
            }
        }

        /// <summary>
        /// Decrypts the specified source stream.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="destination">The destination stream.</param>
        /// <param name="secretDataProvider">The secret data provider.</param>
        /// <returns>A <see cref="DecryptResult" /> indicating the status of the decryption.</returns>
        [CLSCompliant(false)]
        public static DecryptResult Decrypt(
            Stream source, 
            Stream destination,
            IDecryptionSecretDataProvider secretDataProvider)
        {
            return new DecryptResult(false, false, false, null);
        }

        /// <summary>
        /// Verifies the signature on the specified source file.
        /// </summary>
        /// <param name="sourceFileName">Name of the source file.</param>
        /// <param name="destinationFileName">Name of the destination file.</param>
        /// <param name="publicKeyRingFileName">Name of the public key ring file.</param>
        /// <returns>A <see cref="DecryptResult" /> indicating the status of the verification.</returns>
        public static DecryptResult Verify(
            string sourceFileName, 
            string destinationFileName, 
            string publicKeyRingFileName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Verifies the specified detached signature file.
        /// </summary>
        /// <param name="sourceFileName">Name of the source file.</param>
        /// <param name="signatureFileName">Name of the signature file.</param>
        /// <param name="publicKeyRingFileName">Name of the public key ring file.</param>
        /// <returns>A <see cref="DecryptResult" /> indicating the status of the verification.</returns>
        public static bool VerifyDetached(
            string sourceFileName,
            string signatureFileName,
            string publicKeyRingFileName
            )
        {
            throw new NotImplementedException();
        }
    }
}
