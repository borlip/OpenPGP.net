using NUnit.Framework;
using OpenPGP.Core;
using OpenPGPTestingHelpers;

namespace OpenPGPTest.Core
{
    [TestFixture]
    public class BinaryStreamSearcherTest : BaseFixture
    {
        [Test]
        public void IndexOfStringShouldReturnNegativeOneIfStringNotFound()
        {
            var searchStrings = new[] { "this is not", "found in the", "poem text" };
            foreach (var searchString in searchStrings)
            {
                RunIndexOfStringTest("Plaintext003.txt", searchString, -1);
            }

        }

        [Test]
        public void IndexOfStringShouldReturnPositionOfString()
        {
            RunIndexOfStringTest("Plaintext001.txt", "To be, or not to be", 0);
            RunIndexOfStringTest("Plaintext001.txt", "undiscovered country", 1025);
            RunIndexOfStringTest("Plaintext001.txt", "name of action.", 1424);
            RunIndexOfStringTest("Plaintext002.txt", "silken sad uncertain rustling", 689);
            RunIndexOfStringTest("Plaintext003.txt", "no law", 20);
        }

        private static void RunIndexOfStringTest(string resourceName, string searchString, int expectedPosition)
        {
            using (var stream = GetTestDataAsStream(resourceName))
            {
                BinaryStreamSearcher.IndexOfString(stream, searchString, 64).ShouldBe(expectedPosition);
            }
        }
    }
}
