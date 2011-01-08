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

            while (true)
            {
                var haveNextExpected = enumExpected.MoveNext();
                var haveNextValue = enumValue.MoveNext();
                Assert.AreEqual(haveNextExpected, haveNextValue);
                if (!haveNextExpected)
                {
                    break;
                }
                Assert.AreEqual(enumExpected.Current, enumValue.Current);
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
