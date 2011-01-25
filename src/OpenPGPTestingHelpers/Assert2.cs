using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace OpenPGPTestingHelpers
{
    public static class Assert2
    {
        public static void AreElementsEqual<T>(IEnumerable<T> expected, IEnumerable<T> value) where T : IComparable
        {
            var enumExpected = expected.GetEnumerator();
            var enumValue = value.GetEnumerator();
            var position = 0;

            while (true)
            {
                var haveNextExpected = enumExpected.MoveNext();
                var haveNextValue = enumValue.MoveNext();
                Assert.AreEqual(haveNextExpected, haveNextValue, "Number of elements differs");
                if (!haveNextExpected)
                {
                    break;
                }
                Assert.AreEqual(enumExpected.Current, enumValue.Current, "Element at position {0} differs", position);
                position++;
            }
        }

        public static void AreElementsEqual<T>(IEnumerable<T> expected, IEnumerable<T> value, int count) where T : IComparable
        {
            var enumExpected = expected.GetEnumerator();
            var enumValue = value.GetEnumerator();
            var position = 0;

            while (position < count)
            {
                var haveNextExpected = enumExpected.MoveNext();
                var haveNextValue = enumValue.MoveNext();
                Assert.AreEqual(haveNextExpected, haveNextValue, "Insufficient elements to compare");
                if (!haveNextExpected)
                {
                    break;
                }
                Assert.AreEqual(enumExpected.Current, enumValue.Current, "Element at position {0} differs", position);
                position++;
            }
        }

        public static void AreBytesEqual(byte[] expected, int expectedOffset, byte[] actual, int actualOffset, int count)
        {
            Assert.LessOrEqual(expectedOffset + count, expected.Length);
            Assert.LessOrEqual(actualOffset + count, actual.Length);

            for (var i = 0; i < count; ++i)
            {
                var expectedPosition = expectedOffset + i;
                var actualPosition = actualOffset + i;
                Assert.AreEqual(expected[expectedPosition], actual[actualPosition],
                                "expected[{0}] does not equal actual[{1}]", expectedPosition, actualPosition);
            }
        }

        public static void ShouldThrow<T>(Action action) where T : Exception
        {
            try
            {
                action();
            }
            catch (T)
            {
                return;
            }

            Assert.Fail("Expected {0} to be thrown", typeof(T).ToString());
        }

        public static void ShouldThrow<T>(Action action, string message) where T : Exception
        {
            try
            {
                action();
            }
            catch (T ex)
            {
                if (ex.Message != message)
                {
                    Assert.Fail("Expected exception with message '{0}' but got '{1}'", message, ex.Message);
                }
                return;
            }

            Assert.Fail("Expected {0} to be thrown", typeof(T).ToString());
        }
    }
}
