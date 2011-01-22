using System;
using OpenPGP;
using OpenPGP.Core;
using OpenPGPTestingHelpers;
using NUnit.Framework;

namespace OpenPGPTest.Core
{
    [TestFixture]
    public class SimpleByteBufferTest
    {
        // Hex poetry from http://daniel.haxx.se/hexpoetry/goodies.html
        // fooled dad added cable 
        // failed decode doc called aid 
        // official bill baffled
        private static readonly byte[] _BunchOfBytes = StringToBytesConverter.ConvertToByteArray("F001EDDADADDEDCAB1E0FA11EDDEC0DED0CCA11EDA1D0FF1C1A1B111BAFF1ED0");

        #region "Exceptions"

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WriteShouldThrowExceptionIfBufferIsNull()
        {
            // ReSharper disable AssignNullToNotNullAttribute
            CreateEmptyBuffer().Write(null, 0, 8);
            // ReSharper restore AssignNullToNotNullAttribute
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void WriteShouldThrowExceptionOnNegativeOffset()
        {
            CreateEmptyBuffer().Write(new byte[8], -1, 8);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void WriteShouldThrowExceptionOnNegativeCount()
        {
            CreateEmptyBuffer().Write(new byte[8], 0, -1);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void WriteShouldThrowExceptionIfAmountToWriteExceedsBufferLength()
        {
            CreateEmptyBuffer().Write(new byte[8], 4, 8);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReadShouldThrowExceptionIfBufferIsNull()
        {
            // ReSharper disable AssignNullToNotNullAttribute
            CreateEmptyBuffer().Read(null, 0, 8);
            // ReSharper restore AssignNullToNotNullAttribute
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ReadShouldThrowExceptionOnNegativeOffset()
        {
            CreateEmptyBuffer().Read(new byte[8], -1, 8);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ReadShouldThrowExceptionOnNegativeCount()
        {
            CreateEmptyBuffer().Read(new byte[8], 0, -1);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ReadShouldThrowExceptionIfAmountToReadExceedsBufferLength()
        {
            CreateEmptyBuffer().Read(new byte[8], 4, 8);
        }

        private static SimpleByteBuffer CreateEmptyBuffer()
        {
            return new SimpleByteBuffer(8);
        }

        #endregion

        [Test]
        public void WriteShouldWriteBytesToTheBuffer()
        {
            var buffer = new SimpleByteBuffer(8);

            buffer.Write(_BunchOfBytes, 0, 4);

            buffer.BufferSize.ShouldBe(8);
            buffer.BytesAvailable.ShouldBe(4);
        }

        [Test]
        public void ReadShouldReadBytesFromTheBuffer()
        {
            var buffer = new SimpleByteBuffer(8);

            var destination = new byte[8];
            buffer.Write(_BunchOfBytes, 0, 8);
            buffer.Read(destination, 0, 8).ShouldBe(8);

            Assert2.AreBytesEqual(_BunchOfBytes, 0, destination, 0, 8);
        }

        [Test]
        public void WriteShouldWriteBytesAtSpecifiedOffset()
        {
            var buffer = new SimpleByteBuffer(8);

            var destination = new byte[8];
            buffer.Write(_BunchOfBytes, 10, 8);
            buffer.Read(destination, 0, 8).ShouldBe(8);

            Assert2.AreBytesEqual(_BunchOfBytes, 10, destination, 0, 8);
        }

        [Test]
        public void ReadShouldReadBytesAtSpecifiedOffset()
        {
            var buffer = new SimpleByteBuffer(8);

            var destination = new byte[8];
            buffer.Write(_BunchOfBytes, 0, 8);
            buffer.Read(destination, 4, 4).ShouldBe(4);

            Assert2.AreBytesEqual(_BunchOfBytes, 0, destination, 4, 4);
        }

        [Test]
        public void MultipleWritesShouldWriteDataSequentially()
        {
            var buffer = new SimpleByteBuffer(8);

            var destination = new byte[8];
            buffer.Write(_BunchOfBytes, 0, 4);
            buffer.Write(_BunchOfBytes, 4, 4);
            buffer.Read(destination, 0, 8).ShouldBe(8);

            Assert2.AreBytesEqual(_BunchOfBytes, 0, destination, 0, 8);
        }

        [Test]
        public void MultipleReadsShouldReadDataSequentially()
        {
            var buffer = new SimpleByteBuffer(8);

            var destination1 = new byte[4];
            var destination2 = new byte[4];
            buffer.Write(_BunchOfBytes, 0, 8);
            buffer.Read(destination1, 0, 4).ShouldBe(4);
            buffer.Read(destination2, 0, 4).ShouldBe(4);

            Assert2.AreBytesEqual(_BunchOfBytes, 0, destination1, 0, 4);
            Assert2.AreBytesEqual(_BunchOfBytes, 4, destination2, 0, 4);
        }

        [Test]
        public void BytesAvailableShouldReturnNumberOfBytesAvailableForReading()
        {
            var buffer = new SimpleByteBuffer(128);

            var destination = new byte[8];
            buffer.Write(_BunchOfBytes, 0, 10);
            buffer.Read(destination, 0, 4);
            buffer.Write(_BunchOfBytes, 0, 20);
            buffer.Read(destination, 0, 8);
            buffer.Write(_BunchOfBytes, 0, 5);
            buffer.Read(destination, 0, 2);

            buffer.BytesAvailable.ShouldBe(35 - 14);
        }

        [Test]
        public void ReadPositionAndWritePositionShouldResetWhenAllBytesAreRead()
        {
            var buffer = new SimpleByteBuffer(128);

            buffer.WritePosition.ShouldBe(0);
            buffer.Write(_BunchOfBytes, 0, 10);
            buffer.WritePosition.ShouldBe(10);
            buffer.Write(_BunchOfBytes, 0, 5);
            buffer.WritePosition.ShouldBe(15);
            buffer.Write(_BunchOfBytes, 0, 20);
            buffer.WritePosition.ShouldBe(35);

            var destination = new byte[20];
            buffer.ReadPosition.ShouldBe(0);
            buffer.Read(destination, 0, 20).ShouldBe(20);
            buffer.ReadPosition.ShouldBe(20);
            buffer.Read(destination, 0, 5).ShouldBe(5);
            buffer.ReadPosition.ShouldBe(25);
            buffer.Read(destination, 0, 10).ShouldBe(10);

            buffer.WritePosition.ShouldBe(0);
            buffer.ReadPosition.ShouldBe(0);
        }

        [Test]
        public void ReadShouldReturnActualNumberOfBytesRead()
        {
            var buffer = new SimpleByteBuffer(8);

            buffer.Write(_BunchOfBytes, 0, 8);
            var destination = new byte[16];
            buffer.Read(destination, 0, 16).ShouldBe(8);
        }

        [Test]
        public void ReadShouldReturnZeroIfNotDataIsAvailable()
        {
            var buffer = new SimpleByteBuffer(8);
            var destination = new byte[16];
            buffer.Read(destination, 0, 16).ShouldBe(0);
        }

        [Test]
        [ExpectedException(typeof(PGPException))]
        public void WriteShouldThrowExceptionIfAttemptingToWriteBeyondEndOfBuffer()
        {
            var buffer = new SimpleByteBuffer(8);
            buffer.Write(_BunchOfBytes, 0, 4);
            buffer.Write(_BunchOfBytes, 4, 8);
        }

    }
}
