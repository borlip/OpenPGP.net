using System;
using OpenPGP;
using OpenPGPTestingHelpers;
using Xunit;
using Xunit.Extensions;

namespace OpenPGPTest
{
    public class PGPTimeTest
    {
        [Theory]
        [InlineData("1969-07-21T02:56:00.0000000")]
        [InlineData("1776-07-04T00:00:00.0000000")]
        [InlineData("1816-07-09T00:00:00.0000000")]
        public void ConstructorShouldThrowExceptionDateIsBefore1970(string dateString)
        {
            Assert.Throws<ArgumentException>(() => new PGPTime(DateTime.Parse(dateString)));
        }

        [Theory]
        [InlineData(0U, "1970-01-01 00:00:00")]
        [InlineData(213412567U, "1976-10-06T01:16:07.0000000")]
        [InlineData(950938829U, "2000-02-19T05:40:29.0000000")]
        [InlineData(958264599U, "2000-05-14T00:36:39.0000000")]
        [InlineData(1019192351U, "2002-04-19T04:59:11.0000000")]
        [InlineData(1406670365U, "2014-07-29T21:46:05.0000000")]
        public void TimeTest(uint time, string expectedDate)
        {
            var expected = DateTime.Parse(expectedDate);

            var pgpTime = new PGPTime(time);

            pgpTime.Time.ShouldBe(time);
            pgpTime.ToDateTime().ShouldBe(expected);
        }
    }
}
