using OpenPGP.Core;
using OpenPGPTestingHelpers;
using Xunit.Extensions;

namespace OpenPGPTest.Core
{
    public class ArmorHelperTest : BaseFixture
    {
        [Theory]
        [InlineData("ArmoredSymmetric01.txt.asc")] 
        public void IsAsciiArmoredShouldReturnTrueIfDataIsAsciiArmored(string resourceName)
        {
            using (var stream = GetTestDataAsStream(resourceName))
            {
                ArmorHelper.IsAsciiArmored(stream).ShouldBeTrue();
            }
        }

        [Theory]
        [InlineData("BinarySymmetric01.txt.gpg")]
        public void IsAsciiArmoredShouldReturnFalseIfDataIsNotAsciiArmored(string resourceName)
        {
            using (var stream = GetTestDataAsStream(resourceName))
            {
                ArmorHelper.IsAsciiArmored(stream).ShouldBeFalse();
            }
        }
    }
}
