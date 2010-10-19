using System;
using System.Collections.Generic;

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
            throw new NotImplementedException();
        }

        public static bool Verify(
            string sourceFileName, 
            string destinationFileName, 
            string publicKeyRingFileName)
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
