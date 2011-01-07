using MbUnit.Framework;
using OpenPGP.Core;
using OpenPGPTestingHelpers;

namespace OpenPGPTest.Core
{
    [TestFixture]
    public class ArmorHelperTest : BaseFixture
    {
        [Test]
        [Row("ArmoredSymmetric01.txt.asc")] 
        public void IsAsciiArmoredShouldReturnTrueIfDataIsAsciiArmored(string resourceName)
        {
            using (var stream = GetTestDataAsStream(resourceName))
            {
                ArmorHelper.IsAsciiArmored(stream).ShouldBeTrue();
            }
        }

        [Test]
        [Row("BinarySymmetric01.txt.gpg")]
        public void IsAsciiArmoredShouldReturnFalseIfDataIsNotAsciiArmored(string resourceName)
        {
            using (var stream = GetTestDataAsStream(resourceName))
            {
                ArmorHelper.IsAsciiArmored(stream).ShouldBeFalse();
            }
        }
    }
}
