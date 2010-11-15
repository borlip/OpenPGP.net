using System;
using System.IO;
using System.Reflection;
using MbUnit.Framework;
using OpenPGP;
using OpenPGPTestingHelpers;

namespace OpenPGPIntegrationTest
{
    [TestFixture]
    public class SimpleTest
    {
        [Test]
        [Row("BinarySymmetric01.txt.gpg", "Plaintext001.txt", "i have a fever and the only cure is more cowbell")]
        public void DecryptSymmetric(string ciphertextResourceName, string plaintextResourceName, string passphrase)
        {
            var ciphertext = GetNamedResourceAsByteArray(ciphertextResourceName);
            ciphertext.ShouldNotBeNull();
            ciphertext.Length.ShouldBeGreaterThan(0);

            var plaintext = GetNamedResourceAsByteArray(plaintextResourceName);
            plaintext.ShouldNotBeNull();
            plaintext.Length.ShouldBeGreaterThan(0);

            var cipherStream = new MemoryStream(ciphertext);
            var outputStream = new MemoryStream();

            var result = Simple.Decrypt(cipherStream, outputStream, new MockSecretDataProvider(passphrase));

            result.ShouldNotBeNull();
            result.IsSuccessful.ShouldBeTrue();
            result.IsSigned.ShouldBeFalse();
            result.IsSignatureGood.ShouldBeFalse();

            var outputLength = (int)outputStream.Length;
            outputLength.ShouldBe(plaintext.Length);
            var compare = new byte[outputLength];
            outputStream.Seek(0, SeekOrigin.Begin);
            outputStream.Read(compare, 0, outputLength);

            Assert.AreElementsEqual(plaintext, compare);
        }

        private static string GetNamedResourceAsString(string name)
        {
            return ResourceHelper.GetResourceAsString(
                Assembly.GetExecutingAssembly(),
                "OpenPGPIntegrationTest.TestData." + name);
        }

        private static byte[] GetNamedResourceAsByteArray(string name)
        {
            return ResourceHelper.GetResourceAsByteArray(
                Assembly.GetExecutingAssembly(),
                "OpenPGPIntegrationTest.TestData." + name);
        }


    }
}
