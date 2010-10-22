using System;
using System.Collections.Generic;
using System.IO;

namespace OpenPGP
{
    public static class Simple
    {
        public static void Encrypt(
            string sourceFileName,
            string destinationFileName,
            string publicKeyRingFileName,
            IEnumerable<string> recipients)
        {
            throw new NotImplementedException();
        }

        public static void Encrypt(
            string sourceFileName,
            string destinationFileName,
            string passphrase)
        {
            throw new NotImplementedException();
        }

        public static void Sign(
            string sourceFileName,
            string destinationFileName,
            string privateKeyRingFileName,
            string privateKeyIdentifier,
            string privateKeyPassphrase
            )
        {
            throw new NotImplementedException();
        }

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

        public static void EncryptAndSign(
            string sourceFileName, 
            string destinationFileName, 
            string publicKeyRingFileName, 
            IEnumerable<string> recipients, 
            string privateKeyRingFileName, 
            string privateKeyIdentifier, 
            string privateKeyPassphrase)
        {
            throw new NotImplementedException();
        }

        public static void Decrypt(
            string sourceFileName, 
            string destinationFileName, 
            string privateKeyRingFileName, 
            string privateKeyIdentifier, 
            string privateKeyPassphrase)
        {
            throw new NotImplementedException();
        }

        public static void Decrypt(
            string sourceFileName, 
            string destinationFileName, 
            string passphrase)
        {
            using (var source = new FileStream(sourceFileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var destination = new FileStream(destinationFileName, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    Decrypt(source, destination, passphrase);
                }
            }
        }

        public static void Decrypt(
                Stream sourceStream,
                Stream destinationstream,
                string passphrase
            )
        {
            throw new NotImplementedException();
        }

        public static bool Verify(
            string sourceFileName, 
            string destinationFileName, 
            string publicKeyRingFileName)
        {
            throw new NotImplementedException();
        }

        public static bool VerifyDetached(
            string sourceFileName,
            string signatureFileName,
            string publicKeyRingFileName
            )
        {
            throw new NotImplementedException();
        }

        public static bool DecryptAndVerify(
            string sourceFileName, 
            string destinationFileName, 
            string publicKeyRingFileName, 
            string privateKeyRingFileName, 
            string privateKeyIdentifier, 
            string privateKeyPassphrase)
        {
            throw new NotImplementedException();
        }
    }
}
