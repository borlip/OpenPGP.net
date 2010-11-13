using MbUnit.Framework;
using OpenPGP.Core;
using OpenPGPTestingHelpers;

namespace OpenPGPTest.Core
{
    // Test values gratefully obtained from http://www.di-mgt.com.au/src/basCRC24.txt
    [TestFixture]
    public class Crc24ComputerTest
    {
        [Test]
        [ExpectedArgumentNullException]
        public void ComputeCrcShouldThrowExceptionIfInputIsNull()
        {
            Crc24Computer.ComputeCrc(null);
        }

        [Test]
        public void ComputeCrcShouldReturn0ForEmptyInput()
        {
            Crc24Computer.ComputeCrc(new byte[0]).ShouldBe(0);
        }

        [Test]
        [Row("3F214365876616AB15387D5D59", 0xBA0568)]
        [Row("89003F0305013E978A669E02D8AE8DFD6EDE11027520009E2B90532BFD46E8FF1305758BE8DEC71C2C50FCCB009F6F6D5A91A80B89B7D570A6FE382BDEC5951426A6CD", 0x5982EA)]
        public void TestComputeCrc(string input, long expected)
        {
            var bytes = StringToBytesConverter.ConvertToByteArray(input);

            Crc24Computer.ComputeCrc(bytes).ShouldBe(expected);
        }

        [Test]
        [Row("hello world", 0xB03CB7)]
        [Row("Hello world", 0xEDAB02)]
        [Row("123456789", 0x21CF02)]
        public void TestComputeCrcTextStrings(string input, long expected)
        {
            var bytes = System.Text.Encoding.ASCII.GetBytes(input);

            Crc24Computer.ComputeCrc(bytes).ShouldBe(expected);
        }
    }
}
