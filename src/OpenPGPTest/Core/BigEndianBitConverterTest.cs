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
        [Row("0000", 0x0000)]
        [Row("FFFF", 0xFFFF)]
        [Row("1234", 0x1234)]
        [Row("DEAD", 0xDEAD)]
        [Row("BEEF", 0xBEEF)]
        public void TestToUInt16(string hex, ushort expected)
        {
            var bytes = ConvertToByteArray(hex);

            var result = BigEndianBitConverter.ToUInt16(bytes, 0);

            result.ShouldBe(expected);
        }

        public void TestToUInt32(string hex, uint expected)
        {
            var bytes = ConvertToByteArray(hex);

            var result = BigEndianBitConverter.ToUInt32(bytes, 0);

            result.ShouldBe(expected);
        }

        [Test]
        [Row("0000000000000000", 0x0000000000000000)]
        [Row("FFFFFFFFFFFFFFFF", 0xFFFFFFFFFFFFFFFF)]
        [Row("123456789ABCDEF0", 0x123456789ABCDEF0)]
        [Row("DEADBEEFFEEDBABE", 0xDEADBEEFFEEDBABE)]
        public void TestToUInt64(string hex, ulong expected)
        {
            var bytes = ConvertToByteArray(hex);

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
            var expectedBytes = ConvertToByteArray(expectedHex);

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
            var expectedBytes = ConvertToByteArray(expectedHex);

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
            var expectedBytes = ConvertToByteArray(expectedHex);

            var result = BigEndianBitConverter.GetBytes(value);

            Assert.AreElementsEqual(expectedBytes, result);
        }

        private static byte[] ConvertToByteArray(string hex)
        {
            var result = new byte[hex.Length/2];
            for (var i = 0; i < hex.Length; i += 2)
            {
                result[i/2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return result;
        }
    }
}
