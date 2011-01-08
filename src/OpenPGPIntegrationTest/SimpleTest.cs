using System.IO;
using NUnit.Framework;
using OpenPGP;
using OpenPGPTestingHelpers;

namespace OpenPGPIntegrationTest
{
    [TestFixture]
    public class SimpleTest : BaseFixture
    {
        [Test]
        public void DecryptSymmetric()
        {
            RunDecryptSymmetric("BinarySymmetric01.txt.gpg", "Plaintext001.txt", "i have a fever and the only cure is more cowbell");
        }

        private static void RunDecryptSymmetric(string ciphertextResourceName, string plaintextResourceName, string passphrase)
        {
            var ciphertext = GetTestDataAsByteArray(ciphertextResourceName);
            ciphertext.ShouldNotBeNull();
            ciphertext.Length.ShouldBeGreaterThan(0);

            var plaintext = GetTestDataAsByteArray(plaintextResourceName);
            plaintext.ShouldNotBeNull();
            plaintext.Length.ShouldBeGreaterThan(0);

            var cipherStream = new MemoryStream(ciphertext);
            var outputStream = new MemoryStream();

            var result = Simple.Decrypt(cipherStream, outputStream, new MockSecretDataProvider(passphrase));

            result.ShouldNotBeNull("Result of symmetric decryption is null");
            result.IsSuccessful.ShouldBeTrue("Symmetric decryption failed");
            result.IsSigned.ShouldBeFalse("Expected no signature, but found one");
            result.IsSignatureGood.ShouldBeFalse("Expected bad signature, but found good one");

            var outputLength = (int)outputStream.Length;
            outputLength.ShouldBe(plaintext.Length);
            var compare = new byte[outputLength];
            outputStream.Seek(0, SeekOrigin.Begin);
            outputStream.Read(compare, 0, outputLength);

            Assert2.AreElementsEqual(plaintext, compare);
        }
    }
}
