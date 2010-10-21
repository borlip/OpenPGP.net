using System;
using MbUnit.Framework;
using OpenPGP.Core;

namespace OpenPGPTest.Core
{
    [TestFixture]
    public class PGPTimeTest
    {
        [Test]
        [ExpectedArgumentException]
        [Row("1969-07-21T02:56:00.0000000")]
        [Row("1776-07-04T00:00:00.0000000")]
        [Row("1816-07-09T00:00:00.0000000")]
        public void ConstructorShouldThrowExceptionDateIsBefore1970(string dateString)
        {
            new PGPTime(DateTime.Parse(dateString));
        }

        [Test]
        [Row(0, "1970-01-01 00:00:00")]
        [Row(213412567, "1976-10-06T01:16:07.0000000")]
        [Row(950938829, "2000-02-19T05:40:29.0000000")]
        [Row(958264599, "2000-05-14T00:36:39.0000000")]
        [Row(1019192351, "2002-04-19T04:59:11.0000000")]
        [Row(1406670365, "2014-07-29T21:46:05.0000000")]
        public void TimeTest(uint time, string expectedDate)
        {
            var expected = DateTime.Parse(expectedDate);

            var pgpTime = new PGPTime(time);

            pgpTime.Time.ShouldBe(time);
            pgpTime.ToDateTime().ShouldBe(expected);
        }
    }
}
