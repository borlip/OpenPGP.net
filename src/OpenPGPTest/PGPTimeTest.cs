using System;
using NUnit.Framework;
using OpenPGP;
using OpenPGPTestingHelpers;

namespace OpenPGPTest
{
    [TestFixture]
    public class PGPTimeTest
    {
        [Test]
        public void ConstructorShouldThrowExceptionDateIsBefore1970()
        {
            Assert2.ShouldThrow<ArgumentException>(() => new PGPTime(DateTime.Parse("1969-07-21T02:56:00.0000000")));
            Assert2.ShouldThrow<ArgumentException>(() => new PGPTime(DateTime.Parse("1776-07-04T00:00:00.0000000")));
            Assert2.ShouldThrow<ArgumentException>(() => new PGPTime(DateTime.Parse("1816-07-09T00:00:00.0000000")));
        }

        [Test]
        public void TimeTest()
        {
            RunTimeTest(0U, "1970-01-01 00:00:00");
            RunTimeTest(213412567U, "1976-10-06T01:16:07.0000000");
            RunTimeTest(950938829U, "2000-02-19T05:40:29.0000000");
            RunTimeTest(958264599U, "2000-05-14T00:36:39.0000000");
            RunTimeTest(1019192351U, "2002-04-19T04:59:11.0000000");
            RunTimeTest(1406670365U, "2014-07-29T21:46:05.0000000");
        }

        private static void RunTimeTest(uint time, string expectedDate)
        {
            var expected = DateTime.Parse(expectedDate);
            var pgpTime = new PGPTime(time);

            pgpTime.Time.ShouldBe(time);
            pgpTime.ToDateTime().ShouldBe(expected);
        }
    }
}
