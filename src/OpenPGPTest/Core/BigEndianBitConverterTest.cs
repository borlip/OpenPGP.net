using System;
using OpenPGP.Core;
using OpenPGPTestingHelpers;
using Xunit;
using Xunit.Extensions;

namespace OpenPGPTest.Core
{
    public class BigEndianBitConverterTest
    {
        [Fact]
        public void ToUInt16ShouldThrowExceptionOnNullInput()
        {
            Assert.Throws<ArgumentNullException>(() => BigEndianBitConverter.ToUInt16(null, 0));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(100)]
        public void ToUInt16ShouldThrowExceptionOnInvalidStartIndex(int startIndex)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => BigEndianBitConverter.ToUInt16(new byte[2], startIndex));
        }

        [Theory]
        [InlineData(2, 1)]
        [InlineData(10, 9)]
        [InlineData(77, 76)]
        public void ToUInt16ShouldThrowExceptionIfNotEnoughBytes(int numberOfBytes, int testIndex)
        {
            Assert.Throws<ArgumentException>(() => BigEndianBitConverter.ToUInt16(new byte[numberOfBytes], testIndex));
        }

        [Theory]
        [InlineData("0000", 0x0000)]
        [InlineData("FFFF", 0xFFFF)]
        [InlineData("1234", 0x1234)]
        [InlineData("DEAD", 0xDEAD)]
        [InlineData("BEEF", 0xBEEF)]
        public void TestToUInt16(string hex, int expected)
        {
            var bytes = StringToBytesConverter.ConvertToByteArray(hex);

            var result = BigEndianBitConverter.ToUInt16(bytes, 0);

            result.ShouldBe((ushort)expected);
        }
        
        [Fact]
        public void ToUInt32ShouldThrowExceptionOnNullInput()
        {
            Assert.Throws<ArgumentNullException>(() => BigEndianBitConverter.ToUInt32(null, 0));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(100)]
        public void ToUInt32ShouldThrowExceptionOnInvalidStartIndex(int startIndex)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => BigEndianBitConverter.ToUInt32(new byte[2], startIndex));
        }

        [Theory]
        [InlineData(4, 1)]
        [InlineData(10, 7)]
        [InlineData(77, 74)]
        public void ToUInt32ShouldThrowExceptionIfNotEnoughBytes(int numberOfBytes, int testIndex)
        {
            Assert.Throws<ArgumentException>(() => BigEndianBitConverter.ToUInt32(new byte[numberOfBytes], testIndex));
        }

        [Theory]
        [InlineData("00000000", 0x00000000U)]
        [InlineData("FFFFFFFF", 0xFFFFFFFFU)]
        [InlineData("12345678", 0x12345678U)]
        [InlineData("DEADBEEF", 0xDEADBEEFU)]
        [InlineData("FEEDBABE", 0xFEEDBABEU)]
        public void TestToUInt32(string hex, uint expected)
        {
            var bytes = StringToBytesConverter.ConvertToByteArray(hex);

            var result = BigEndianBitConverter.ToUInt32(bytes, 0);

            result.ShouldBe(expected);
        }

        [Fact]
        public void ToUInt64ShouldThrowExceptionOnNullInput()
        {
            Assert.Throws<ArgumentNullException>(() => BigEndianBitConverter.ToUInt64(null, 0));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(100)]
        public void ToUInt64ShouldThrowExceptionOnInvalidStartIndex(int startIndex)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => BigEndianBitConverter.ToUInt64(new byte[2], startIndex));
        }

        [Theory]
        [InlineData(8, 1)]
        [InlineData(10, 3)]
        [InlineData(77, 70)]
        public void ToUInt64ShouldThrowExceptionIfNotEnoughBytes(int numberOfBytes, int testIndex)
        {
            Assert.Throws<ArgumentException>(() => BigEndianBitConverter.ToUInt64(new byte[numberOfBytes], testIndex));
        }

        [Theory]
        [InlineData("0000000000000000", 0x0000000000000000UL)]
        [InlineData("FFFFFFFFFFFFFFFF", 0xFFFFFFFFFFFFFFFFUL)]
        [InlineData("123456789ABCDEF0", 0x123456789ABCDEF0UL)]
        [InlineData("DEADBEEFFEEDBABE", 0xDEADBEEFFEEDBABEUL)]
        public void TestToUInt64(string hex, ulong expected)
        {
            var bytes = StringToBytesConverter.ConvertToByteArray(hex);

            var result = BigEndianBitConverter.ToUInt64(bytes, 0);

            result.ShouldBe(expected);
        }

        [Theory]
        [InlineData(0x0000, "0000")]
        [InlineData(0xFFFF, "FFFF")]
        [InlineData(0x1234, "1234")]
        [InlineData(0xDEAD, "DEAD")]
        [InlineData(0xBEEF, "BEEF")]
        public void TestGetBytesToUInt16(int value, string expectedHex)
        {
            var expectedBytes = StringToBytesConverter.ConvertToByteArray(expectedHex);

            var result = BigEndianBitConverter.GetBytes((ushort)value);

            Assert2.AreElementsEqual(expectedBytes, result);
        }

        [Theory]
        [InlineData(0x00000000U, "00000000")]
        [InlineData(0xFFFFFFFFU, "FFFFFFFF")]
        [InlineData(0x12345678U, "12345678")]
        [InlineData(0xDEADBEEFU, "DEADBEEF")]
        [InlineData(0xFEEDBABEU, "FEEDBABE")]
        public void TestGetBytesToUInt32(uint value, string expectedHex)
        {
            var expectedBytes = StringToBytesConverter.ConvertToByteArray(expectedHex);

            var result = BigEndianBitConverter.GetBytes(value);

            Assert2.AreElementsEqual(expectedBytes, result);
        }

        [Theory]
        [InlineData(0x0000000000000000UL, "0000000000000000")]
        [InlineData(0xFFFFFFFFFFFFFFFFUL, "FFFFFFFFFFFFFFFF")]
        [InlineData(0x123456789ABCDEF0UL, "123456789ABCDEF0")]
        [InlineData(0xDEADBEEFFEEDBABEUL, "DEADBEEFFEEDBABE")]
        public void TestGetBytesToUInt64(ulong value, string expectedHex)
        {
            var expectedBytes = StringToBytesConverter.ConvertToByteArray(expectedHex);

            var result = BigEndianBitConverter.GetBytes(value);

            Assert2.AreElementsEqual(expectedBytes, result);
        }
    }
}
