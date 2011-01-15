using System;
using System.IO;
using NUnit.Framework;
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
            var input = GetTestDataAsStream("ArmoredSymmetric01.txt.asc");
            var armorStream = new AsciiArmorReaderStream(input);
            var buffer = new byte[8];

            armorStream.Read(buffer, 0, 1);

            armorStream.Headers.Count.ShouldBe(1);
            armorStream.Headers.ContainsKey(AsciiArmorConstants.VersionHeader).ShouldBeTrue();
            armorStream.Headers[AsciiArmorConstants.VersionHeader].ShouldBe("GnuPG v1.4.11 (MingW32)");
        }

    }
}
