using System;
using OpenPGP.Core;
using OpenPGPTestingHelpers;
using Xunit;
using Xunit.Extensions;

namespace OpenPGPTest.Core
{
    // Test values gratefully obtained from http://www.di-mgt.com.au/src/basCRC24.txt
    public class Crc24ComputerTest
    {
        [Fact]
        public void ComputeCrcShouldThrowExceptionIfInputIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => Crc24Computer.ComputeCrc(null));
        }

        [Fact]
        public void ComputeCrcShouldReturn0ForEmptyInput()
        {
            Crc24Computer.ComputeCrc(new byte[0]).ShouldBe(0);
        }

        [Theory]
        [InlineData("3F214365876616AB15387D5D59", 0xBA0568)]
        [InlineData("89003F0305013E978A669E02D8AE8DFD6EDE11027520009E2B90532BFD46E8FF1305758BE8DEC71C2C50FCCB009F6F6D5A91A80B89B7D570A6FE382BDEC5951426A6CD", 0x5982EA)]
        public void TestComputeCrc(string input, long expected)
        {
            var bytes = StringToBytesConverter.ConvertToByteArray(input);

            Crc24Computer.ComputeCrc(bytes).ShouldBe(expected);
        }

        [Theory]
        [InlineData("3F21", "436587", "6616AB15", "387D5D59", 0xBA0568)]
        [InlineData("89003F030501", "3E978A669E02D8AE", "8DFD6EDE11027520009E2B90532BFD46", "E8FF1305758BE8DEC71C2C50FCCB009F6F6D5A91A80B89B7D570A6FE382BDEC5951426A6CD", 0x5982EA)]
        public void TestComputeCrcSplit(string input1, string input2, string input3, string input4, long expected)
        {
            var bytes1 = StringToBytesConverter.ConvertToByteArray(input1);
            var bytes2 = StringToBytesConverter.ConvertToByteArray(input2);
            var bytes3 = StringToBytesConverter.ConvertToByteArray(input3);
            var bytes4 = StringToBytesConverter.ConvertToByteArray(input4);

            var crc = Crc24Computer.ComputeCrc(bytes1);
            crc = Crc24Computer.ComputeCrc(bytes2, crc);
            crc = Crc24Computer.ComputeCrc(bytes3, crc);
            crc = Crc24Computer.ComputeCrc(bytes4, crc);

            crc.ShouldBe(expected);
        }

        [Theory]
        [InlineData("hello world", 0xB03CB7)]
        [InlineData("Hello world", 0xEDAB02)]
        [InlineData("123456789", 0x21CF02)]
        public void TestComputeCrcTextStrings(string input, long expected)
        {
            var bytes = System.Text.Encoding.ASCII.GetBytes(input);

            Crc24Computer.ComputeCrc(bytes).ShouldBe(expected);
        }
    }
}
