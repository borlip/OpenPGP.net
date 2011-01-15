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

        [Test]
        public void IsAsciiArmorHeaderLineShouldReturnTrueIfDataIsAnAsciiArmorHeaderLine()
        {
            ArmorHelper.IsAsciiArmorHeaderLine(AsciiArmorConstants.MessageHeaderLine).ShouldBeTrue();
            ArmorHelper.IsAsciiArmorHeaderLine(AsciiArmorConstants.SignatureHeaderLine).ShouldBeTrue();
            ArmorHelper.IsAsciiArmorHeaderLine(AsciiArmorConstants.PrivateKeyBlockHeaderLine).ShouldBeTrue();
            ArmorHelper.IsAsciiArmorHeaderLine(AsciiArmorConstants.PublicKeyBlockHeaderLine).ShouldBeTrue();
            ArmorHelper.IsAsciiArmorHeaderLine("-----BEGIN PGP MESSAGE, PART 1/3-----").ShouldBeTrue();
            ArmorHelper.IsAsciiArmorHeaderLine("-----BEGIN PGP MESSAGE, PART 2-----").ShouldBeTrue();
        }

        [Test]
        public void IsAsciiArmorHeaderLineShouldReturnFalseIfDataIsNotAnAsciiArmorHeaderLine()
        {
            ArmorHelper.IsAsciiArmorHeaderLine("BEGIN PGP MESSAGE").ShouldBeFalse();
            ArmorHelper.IsAsciiArmorHeaderLine("complete garbage").ShouldBeFalse();
        }
    }
}
