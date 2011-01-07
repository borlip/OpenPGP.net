using System;
using System.Collections.Generic;
using Xunit;

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
                Assert.Equal(haveNextExpected, haveNextValue);
                if (!haveNextExpected)
                {
                    break;
                }
                Assert.Equal(enumExpected.Current, enumValue.Current);
            }
        }
    }
}
