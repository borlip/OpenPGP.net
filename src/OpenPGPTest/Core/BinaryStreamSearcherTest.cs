using MbUnit.Framework;
using OpenPGP.Core;
using OpenPGPTestingHelpers;

namespace OpenPGPTest.Core
{
    [TestFixture]
    public class BinaryStreamSearcherTest : BaseFixture
    {
        [Test]
        [Row("this is not")]
        [Row("found in the")]
        [Row("poem text")]
        public void IndexOfStringShouldReturnNegativeOneIfStringNotFound(string searchString)
        {
            using (var stream = GetTestDataAsStream("Plaintext003.txt"))
            {
                BinaryStreamSearcher.IndexOfString(stream, searchString).ShouldBe(-1);
            }
        }

        [Test]
        [Row("Plaintext001.txt", "To be, or not to be", 0)]
        [Row("Plaintext001.txt", "undiscovered country", 1025)]
        [Row("Plaintext001.txt", "name of action.", 1424)]
        [Row("Plaintext002.txt", "silken sad uncertain rustling", 689)]
        [Row("Plaintext003.txt", "no law", 20)]
        public void IndexOfStringShouldReturnPositionOfString(string resourceName, string searchString, int expectedPosition)
        {
            using (var stream = GetTestDataAsStream(resourceName))
            {
                BinaryStreamSearcher.IndexOfString(stream, searchString, 64).ShouldBe(expectedPosition);
            }
        }
    }
}
