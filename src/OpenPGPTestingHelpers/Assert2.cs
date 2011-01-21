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
    }
}
