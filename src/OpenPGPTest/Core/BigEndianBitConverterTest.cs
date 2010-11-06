using System;
using MbUnit.Framework;
using OpenPGP.Core;
using OpenPGPTestingHelpers;

namespace OpenPGPTest.Core
{
    [TestFixture]
    public class BigEndianBitConverterTest
    {
        [Test]
        [ExpectedArgumentNullException]
        public void ToUInt16ShouldThrowExceptionOnNullInput()
        {
            BigEndianBitConverter.ToUInt16(null, 0);
        }

        [Test]
        [ExpectedArgumentOutOfRangeException]
        [Row(-1)]
        [Row(100)]
        public void ToUInt16ShouldThrowExceptionOnInvalidStartIndex(int startIndex)
        {
            BigEndianBitConverter.ToUInt16(new byte[2], startIndex);
        }

        [Test]
        [ExpectedArgumentException]
        [Row(2, 1)]
        [Row(10, 9)]
        [Row(77, 76)]
        public void ToUInt16ShouldThrowExceptionIfNotEnoughBytes(int numberOfBytes, int testIndex)
        {
            BigEndianBitConverter.ToUInt16(new byte[numberOfBytes], testIndex);
        }

        [Test]
        [Row("0000", 0x0000)]
        [Row("FFFF", 0xFFFF)]
        [Row("1234", 0x1234)]
        [Row("DEAD", 0xDEAD)]
        [Row("BEEF", 0xBEEF)]
        public void TestToUInt16(string hex, ushort expected)
        {
            var bytes = StringToBytesConverter.ConvertToByteArray(hex);

            var result = BigEndianBitConverter.ToUInt16(bytes, 0);

            result.ShouldBe(expected);
        }
        
        [Test]
        [ExpectedArgumentNullException]
        public void ToUInt32ShouldThrowExceptionOnNullInput()
        {
            BigEndianBitConverter.ToUInt32(null, 0);
        }

        [Test]
        [ExpectedArgumentOutOfRangeException]
        [Row(-1)]
        [Row(100)]
        public void ToUInt32ShouldThrowExceptionOnInvalidStartIndex(int startIndex)
        {
            BigEndianBitConverter.ToUInt32(new byte[2], startIndex);
        }

        [Test]
        [ExpectedArgumentException]
        [Row(4, 1)]
        [Row(10, 7)]
        [Row(77, 74)]
        public void ToUInt32ShouldThrowExceptionIfNotEnoughBytes(int numberOfBytes, int testIndex)
        {
            BigEndianBitConverter.ToUInt32(new byte[numberOfBytes], testIndex);
        }

        [Test]
        [Row("00000000", 0x00000000)]
        [Row("FFFFFFFF", 0xFFFFFFFF)]
        [Row("12345678", 0x12345678)]
        [Row("DEADBEEF", 0xDEADBEEF)]
        [Row("FEEDBABE", 0xFEEDBABE)]
        public void TestToUInt32(string hex, uint expected)
        {
            var bytes = StringToBytesConverter.ConvertToByteArray(hex);

            var result = BigEndianBitConverter.ToUInt32(bytes, 0);

            result.ShouldBe(expected);
        }

        [Test]
        [ExpectedArgumentNullException]
        public void ToUInt64ShouldThrowExceptionOnNullInput()
        {
            BigEndianBitConverter.ToUInt64(null, 0);
        }

        [Test]
        [ExpectedArgumentOutOfRangeException]
        [Row(-1)]
        [Row(100)]
        public void ToUInt64ShouldThrowExceptionOnInvalidStartIndex(int startIndex)
        {
            BigEndianBitConverter.ToUInt64(new byte[2], startIndex);
        }

        [Test]
        [ExpectedArgumentException]
        [Row(8, 1)]
        [Row(10, 3)]
        [Row(77, 70)]
        public void ToUInt64ShouldThrowExceptionIfNotEnoughBytes(int numberOfBytes, int testIndex)
        {
            BigEndianBitConverter.ToUInt64(new byte[numberOfBytes], testIndex);
        }

        [Test]
        [Row("0000000000000000", 0x0000000000000000)]
        [Row("FFFFFFFFFFFFFFFF", 0xFFFFFFFFFFFFFFFF)]
        [Row("123456789ABCDEF0", 0x123456789ABCDEF0)]
        [Row("DEADBEEFFEEDBABE", 0xDEADBEEFFEEDBABE)]
        public void TestToUInt64(string hex, ulong expected)
        {
            var bytes = StringToBytesConverter.ConvertToByteArray(hex);

            var result = BigEndianBitConverter.ToUInt64(bytes, 0);

            result.ShouldBe(expected);
        }

        [Test]
        [Row(0x0000, "0000")]
        [Row(0xFFFF, "FFFF")]
        [Row(0x1234, "1234")]
        [Row(0xDEAD, "DEAD")]
        [Row(0xBEEF, "BEEF")]
        public void TestGetBytesToUInt16(ushort value, string expectedHex)
        {
            var expectedBytes = StringToBytesConverter.ConvertToByteArray(expectedHex);

            var result = BigEndianBitConverter.GetBytes(value);

            Assert.AreElementsEqual(expectedBytes, result);
        }

        [Test]
        [Row(0x00000000, "00000000")]
        [Row(0xFFFFFFFF, "FFFFFFFF")]
        [Row(0x12345678, "12345678")]
        [Row(0xDEADBEEF, "DEADBEEF")]
        [Row(0xFEEDBABE, "FEEDBABE")]
        public void TestGetBytesToUInt32(uint value, string expectedHex)
        {
            var expectedBytes = StringToBytesConverter.ConvertToByteArray(expectedHex);

            var result = BigEndianBitConverter.GetBytes(value);

            Assert.AreElementsEqual(expectedBytes, result);
        }

        [Test]
        [Row(0x0000000000000000, "0000000000000000")]
        [Row(0xFFFFFFFFFFFFFFFF, "FFFFFFFFFFFFFFFF")]
        [Row(0x123456789ABCDEF0, "123456789ABCDEF0")]
        [Row(0xDEADBEEFFEEDBABE, "DEADBEEFFEEDBABE")]
        public void TestGetBytesToUInt64(ulong value, string expectedHex)
        {
            var expectedBytes = StringToBytesConverter.ConvertToByteArray(expectedHex);

            var result = BigEndianBitConverter.GetBytes(value);

            Assert.AreElementsEqual(expectedBytes, result);
        }
    }
}
