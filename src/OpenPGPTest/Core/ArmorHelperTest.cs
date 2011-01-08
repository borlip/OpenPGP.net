using NUnit.Framework;
using OpenPGP.Core;
using OpenPGPTestingHelpers;

namespace OpenPGPTest.Core
{
    [TestFixture]
    public class ArmorHelperTest : BaseFixture
    {
        [Test]
        public void IsAsciiArmoredShouldReturnTrueIfDataIsAsciiArmored()
        {
            var testResources = new[] {"ArmoredSymmetric01.txt.asc"};
            foreach (var resourceName in testResources)
            {
                using (var stream = GetTestDataAsStream(resourceName))
                {
                    ArmorHelper.IsAsciiArmored(stream).ShouldBeTrue();
                }
            }
        }

        [Test]
        public void IsAsciiArmoredShouldReturnFalseIfDataIsNotAsciiArmored()
        {
            var testResources = new[] { "BinarySymmetric01.txt.gpg" };
            foreach (var resourceName in testResources)
            {
                using (var stream = GetTestDataAsStream(resourceName))
                {
                    ArmorHelper.IsAsciiArmored(stream).ShouldBeFalse();
                }
            }
        }
    }
}
