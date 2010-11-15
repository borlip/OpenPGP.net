using OpenPGP;

namespace OpenPGPIntegrationTest
{
    class MockSecretDataProvider : IDecryptionSecretDataProvider
    {
        public string Passphrase { get; set; }

        public MockSecretDataProvider(string passphrase)
        {
            Passphrase = passphrase;
        }

        public string GetSymmetricDecryptionPassphrase()
        {
            return Passphrase;
        }

        public string GetPassphraseForPrivateKey(KeyIdentifier keyIdentifier)
        {
            return Passphrase;
        }
    }
}