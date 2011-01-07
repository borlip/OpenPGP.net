using OpenPGP.Core;
using OpenPGPTestingHelpers;
using Xunit.Extensions;

namespace OpenPGPTest.Core
{
    public class BinaryStreamSearcherTest : BaseFixture
    {
        [Theory]
        [InlineData("this is not")]
        [InlineData("found in the")]
        [InlineData("poem text")]
        public void IndexOfStringShouldReturnNegativeOneIfStringNotFound(string searchString)
        {
            using (var stream = GetTestDataAsStream("Plaintext003.txt"))
            {
                BinaryStreamSearcher.IndexOfString(stream, searchString).ShouldBe(-1);
            }
        }

        [Theory]
        [InlineData("Plaintext001.txt", "To be, or not to be", 0)]
        [InlineData("Plaintext001.txt", "undiscovered country", 1025)]
        [InlineData("Plaintext001.txt", "name of action.", 1424)]
        [InlineData("Plaintext002.txt", "silken sad uncertain rustling", 689)]
        [InlineData("Plaintext003.txt", "no law", 20)]
        public void IndexOfStringShouldReturnPositionOfString(string resourceName, string searchString, int expectedPosition)
        {
            using (var stream = GetTestDataAsStream(resourceName))
            {
                BinaryStreamSearcher.IndexOfString(stream, searchString, 64).ShouldBe(expectedPosition);
            }
        }
    }
}
