using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using OpenPGP;
using OpenPGP.Core;
using OpenPGPTestingHelpers;

namespace OpenPGPTest.Core
{
    [TestFixture]
    public class AsciiArmorReaderStreamTest : BaseFixture
    {
        #region "Exceptions"

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorShouldThrowExceptionOnNullInput()
        {
            new AsciiArmorReaderStream(null);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void SeekShouldThrowException()
        {
            CreateEmptyStream().Seek(0, SeekOrigin.Begin);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void SetLengthShouldThrowException()
        {
            CreateEmptyStream().SetLength(0);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void WriteShouldThrowException()
        {
            CreateEmptyStream().Write(new byte[8], 0, 1);
        }

        [Test]
        [ExpectedException(typeof(ObjectDisposedException))]
        public void ReadShouldThrowExceptionIfStreamIsDisposed()
        {
            var armorStream = CreateEmptyStream();
            armorStream.Dispose();
            armorStream.Read(new byte[8], 0, 8);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReadShouldThrowExceptionIfBufferIsNull()
        {
// ReSharper disable AssignNullToNotNullAttribute
            CreateEmptyStream().Read(null, 0, 8);
// ReSharper restore AssignNullToNotNullAttribute
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ReadShouldThrowExceptionOnNegativeOffset()
        {
            CreateEmptyStream().Read(new byte[8], -1, 8);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ReadShouldThrowExceptionOnNegativeCount()
        {
            CreateEmptyStream().Read(new byte[8], 0, -1);
        }

        private static AsciiArmorReaderStream CreateEmptyStream()
        {
            return new AsciiArmorReaderStream(new MemoryStream());
        }

        #endregion

        [Test]
        public void VerifyCapabilitiesProperties()
        {
            var test = CreateEmptyStream();
            test.CanRead.ShouldBeTrue();
            test.CanSeek.ShouldBeFalse();
            test.CanWrite.ShouldBeFalse();
        }

        [Test]
        public void ReadShouldLoadHeadersOnFirstRead()
        {
            RunHeaderReadTest("ArmoredSymmetric01.txt.asc", 1,
                              new[]
                                  {
                                      new KeyValuePair<string, string>(
                                          AsciiArmorConstants.VersionHeader,
                                          "GnuPG v1.4.11 (MingW32)")
                                  });

            RunHeaderReadTest("ArmoredSymmetric02.txt.asc", 2,
                              new[]
                                  {
                                      new KeyValuePair<string, string>(
                                          AsciiArmorConstants.CommentHeader,
                                          "This is a comment")
                                  });
        }

        private static void RunHeaderReadTest(string resourceName, int expectedHeaderCount, IEnumerable<KeyValuePair<string, string>> expectedHeaders)
        {
            var input = GetTestDataAsStream(resourceName);
            var armorStream = new AsciiArmorReaderStream(input);
            var buffer = new byte[8];

            armorStream.Read(buffer, 0, 1);

            armorStream.Headers.Count.ShouldBe(expectedHeaderCount);

            foreach (var header in expectedHeaders)
            {
                armorStream.Headers.ContainsKey(header.Key).ShouldBeTrue();
                armorStream.Headers[header.Key].ShouldBe(header.Value);
            }
        }

        [Test]
        [ExpectedException(typeof(PGPException))]
        public void ReadShouldThrowExceptionIfNotArmored()
        {
            var input = GetTestDataAsStream("Plaintext001.txt");
            var armorStream = new AsciiArmorReaderStream(input);
            var buffer = new byte[8];

            armorStream.Read(buffer, 0, 1);
        }
    }
}
