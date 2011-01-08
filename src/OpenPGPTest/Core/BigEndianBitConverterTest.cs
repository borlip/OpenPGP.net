using System;
using NUnit.Framework;
using OpenPGP.Core;
using OpenPGPTestingHelpers;

namespace OpenPGPTest.Core
{
    [TestFixture]
    public class BigEndianBitConverterTest
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ToUInt16ShouldThrowExceptionOnNullInput()
        {
            BigEndianBitConverter.ToUInt16(null, 0);
        }

        [Test]
        public void ToUInt16ShouldThrowExceptionOnInvalidStartIndex()
        {
            Assert2.ShouldThrow<ArgumentOutOfRangeException>(() => BigEndianBitConverter.ToUInt16(new byte[2], -1));
            Assert2.ShouldThrow<ArgumentOutOfRangeException>(() => BigEndianBitConverter.ToUInt16(new byte[2], 100));
        }

        [Test]
        public void ToUInt16ShouldThrowExceptionIfNotEnoughBytes()
        {
            Assert2.ShouldThrow<ArgumentException>(() => BigEndianBitConverter.ToUInt16(new byte[2], 1));
            Assert2.ShouldThrow<ArgumentException>(() => BigEndianBitConverter.ToUInt16(new byte[10], 9));
            Assert2.ShouldThrow<ArgumentException>(() => BigEndianBitConverter.ToUInt16(new byte[77], 76));
        }

        [Test]
        public void TestToUInt16()
        {
            RunTestToUInt16("0000", 0x0000);
            RunTestToUInt16("FFFF", 0xFFFF);
            RunTestToUInt16("1234", 0x1234);
            RunTestToUInt16("DEAD", 0xDEAD);
            RunTestToUInt16("BEEF", 0xBEEF);
        }

        private static void RunTestToUInt16(string hex, int expected)
        {
            var bytes = StringToBytesConverter.ConvertToByteArray(hex);
            var result = BigEndianBitConverter.ToUInt16(bytes, 0);
            result.ShouldBe((ushort)expected);
        }

        [Test]
        public void ToUInt32ShouldThrowExceptionOnNullInput()
        {
            Assert2.ShouldThrow<ArgumentNullException>(() => BigEndianBitConverter.ToUInt32(null, 0));
        }

        [Test]
        public void ToUInt32ShouldThrowExceptionOnInvalidStartIndex()
        {
            Assert2.ShouldThrow<ArgumentOutOfRangeException>(() => BigEndianBitConverter.ToUInt32(new byte[4], -1));
            Assert2.ShouldThrow<ArgumentOutOfRangeException>(() => BigEndianBitConverter.ToUInt32(new byte[4], 100));
        }

        [Test]
        public void ToUInt32ShouldThrowExceptionIfNotEnoughBytes()
        {
            Assert2.ShouldThrow<ArgumentException>(() => BigEndianBitConverter.ToUInt32(new byte[4], 1));
            Assert2.ShouldThrow<ArgumentException>(() => BigEndianBitConverter.ToUInt32(new byte[10], 7));
            Assert2.ShouldThrow<ArgumentException>(() => BigEndianBitConverter.ToUInt32(new byte[77], 74));
        }

        [Test]
        public void TestToUInt32()
        {
            RunTestToUInt32("00000000", 0x00000000U);
            RunTestToUInt32("FFFFFFFF", 0xFFFFFFFFU);
            RunTestToUInt32("12345678", 0x12345678U);
            RunTestToUInt32("DEADBEEF", 0xDEADBEEFU);
            RunTestToUInt32("FEEDBABE", 0xFEEDBABEU);
        }

        private static void RunTestToUInt32(string hex, uint expected)
        {
            var bytes = StringToBytesConverter.ConvertToByteArray(hex);
            var result = BigEndianBitConverter.ToUInt32(bytes, 0);
            result.ShouldBe(expected);
        }

        [Test]
        public void ToUInt64ShouldThrowExceptionOnNullInput()
        {
            Assert2.ShouldThrow<ArgumentNullException>(() => BigEndianBitConverter.ToUInt64(null, 0));
        }

        [Test]
        public void ToUInt64ShouldThrowExceptionOnInvalidStartIndex()
        {
            Assert2.ShouldThrow<ArgumentOutOfRangeException>(() => BigEndianBitConverter.ToUInt64(new byte[8], -1));
            Assert2.ShouldThrow<ArgumentOutOfRangeException>(() => BigEndianBitConverter.ToUInt64(new byte[8], 100));
        }

        [Test]
        public void ToUInt64ShouldThrowExceptionIfNotEnoughBytes()
        {
            Assert2.ShouldThrow<ArgumentException>(() => BigEndianBitConverter.ToUInt64(new byte[8], 1));
            Assert2.ShouldThrow<ArgumentException>(() => BigEndianBitConverter.ToUInt64(new byte[10], 3));
            Assert2.ShouldThrow<ArgumentException>(() => BigEndianBitConverter.ToUInt64(new byte[77], 70));
        }

        [Test]
        public void TestToUInt64()
        {
            RunTestToUInt64("0000000000000000", 0x0000000000000000UL);
            RunTestToUInt64("FFFFFFFFFFFFFFFF", 0xFFFFFFFFFFFFFFFFUL);
            RunTestToUInt64("123456789ABCDEF0", 0x123456789ABCDEF0UL);
            RunTestToUInt64("DEADBEEFFEEDBABE", 0xDEADBEEFFEEDBABEUL);
        }

        private static void RunTestToUInt64(string hex, ulong expected)
        {
            var bytes = StringToBytesConverter.ConvertToByteArray(hex);
            var result = BigEndianBitConverter.ToUInt64(bytes, 0);
            result.ShouldBe(expected);
        }

        [Test]
        public void TestGetBytesToUInt16()
        {
            RunTestGetBytesToUInt16(0x0000, "0000");
            RunTestGetBytesToUInt16(0xFFFF, "FFFF");
            RunTestGetBytesToUInt16(0x1234, "1234");
            RunTestGetBytesToUInt16(0xDEAD, "DEAD");
            RunTestGetBytesToUInt16(0xBEEF, "BEEF");
        }

        private static void RunTestGetBytesToUInt16(int value, string expectedHex)
        {
            var expectedBytes = StringToBytesConverter.ConvertToByteArray(expectedHex);
            var result = BigEndianBitConverter.GetBytes((ushort)value);
            Assert2.AreElementsEqual(expectedBytes, result);
        }

        [Test]
        public void TestGetBytesToUInt32()
        {
            RunTestGetBytesToUInt32(0x00000000U, "00000000");
            RunTestGetBytesToUInt32(0xFFFFFFFFU, "FFFFFFFF");
            RunTestGetBytesToUInt32(0x12345678U, "12345678");
            RunTestGetBytesToUInt32(0xDEADBEEFU, "DEADBEEF");
            RunTestGetBytesToUInt32(0xFEEDBABEU, "FEEDBABE");
        }

        private static void RunTestGetBytesToUInt32(uint value, string expectedHex)
        {
            var expectedBytes = StringToBytesConverter.ConvertToByteArray(expectedHex);
            var result = BigEndianBitConverter.GetBytes(value);
            Assert2.AreElementsEqual(expectedBytes, result);
        }

        [Test]
        public void TestGetBytesToUInt64()
        {
            RunTestGetBytesToUInt64(0x0000000000000000UL, "0000000000000000");
            RunTestGetBytesToUInt64(0xFFFFFFFFFFFFFFFFUL, "FFFFFFFFFFFFFFFF");
            RunTestGetBytesToUInt64(0x123456789ABCDEF0UL, "123456789ABCDEF0");
            RunTestGetBytesToUInt64(0xDEADBEEFFEEDBABEUL, "DEADBEEFFEEDBABE");
        }

        private static void RunTestGetBytesToUInt64(ulong value, string expectedHex)
        {
            var expectedBytes = StringToBytesConverter.ConvertToByteArray(expectedHex);
            var result = BigEndianBitConverter.GetBytes(value);
            Assert2.AreElementsEqual(expectedBytes, result);
        }
    }
}
