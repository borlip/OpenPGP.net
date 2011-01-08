using System;
using NUnit.Framework;

namespace OpenPGPTestingHelpers
{
    /// <summary>
    /// Creates extension methods that make it easier to write assertions.
    /// </summary>
    public static class AssertionExtensions
    {
        public static void ShouldBe<T>(this T item, T expectedValue)
        {
            ShouldBe(item, expectedValue, null, null);
        }

        public static void ShouldBe<T>(this T item, T expectedValue, string messageFormat, params object[] messageArguments)
        {
            Assert.AreEqual(expectedValue, item, messageFormat, messageArguments);
        }

        public static void ShouldNotBe<T>(this T item, T expectedValue)
        {
            ShouldNotBe(item, expectedValue, null, null);
        }

        public static void ShouldNotBe<T>(this T item, T expectedValue, string messageFormat, params object[] messageArguments)
        {
            Assert.AreNotEqual(expectedValue, item, messageFormat, messageArguments);
        }

        public static void ShouldBeSameReference<T>(this T item, T expectedValue) where T : class
        {
            ShouldBeSameReference(item, expectedValue, null, null);
        }

        public static void ShouldBeSameReference<T>(this T item, T expectedValue, string messageFormat, params object[] messageArguments) where T : class
        {
            Assert.AreSame(expectedValue, item, messageFormat, messageArguments);
        }

        public static void ShouldNotBeSameReference<T>(this T item, T expectedValue) where T : class
        {
            ShouldNotBeSameReference(item, expectedValue, null, null);
        }

        public static void ShouldNotBeSameReference<T>(this T item, T expectedValue, string messageFormat, params object[] messageArguments) where T : class
        {
            Assert.AreNotSame(expectedValue, item, messageFormat, messageArguments);
        }

        public static void ShouldBeTrue(this bool item)
        {
            ShouldBeTrue(item, null, null);
        }

        public static void ShouldBeTrue(this bool item, string messageFormat, params object[] messageArguments)
        {
            Assert.IsTrue(item, messageFormat, messageArguments);
        }

        public static void ShouldBeFalse(this bool item)
        {
            ShouldBeFalse(item, null, null);
        }

        public static void ShouldBeFalse(this bool item, string messageFormat, params object[] messageArguments)
        {
            Assert.IsFalse(item, messageFormat, messageArguments);
        }

        public static void ShouldBeNull(this object item)
        {
            ShouldBeNull(item, null, null);
        }

        public static void ShouldBeNull(this object item, string messageFormat, params object[] messageArguments)
        {
            Assert.IsNull(item, messageFormat, messageArguments);
        }

        public static void ShouldNotBeNull(this object item)
        {
            ShouldNotBeNull(item, null, null);
        }

        public static void ShouldNotBeNull(this object item, string messageFormat, params object[] messageArguments)
        {
            Assert.IsNotNull(item, messageFormat, messageArguments);
        }

        public static void ShouldContain(this string item, string expectedSubString)
        {
            ShouldContain(item, expectedSubString, null, null);
        }

        public static void ShouldContain(this string item, string expectedSubString, string messageFormat, params object[] messageArguments)
        {
            Assert.IsTrue(item.IndexOf(expectedSubString) >= 0, messageFormat, messageArguments);
        }

        public static void ShouldNotContain(this string item, string unexpectedSubString)
        {
            ShouldNotContain(item, unexpectedSubString, null, null);
        }

        public static void ShouldNotContain(this string item, string unexpectedSubString, string messageFormat, params object[] messageArguments)
        {
            Assert.IsFalse(item.IndexOf(unexpectedSubString) >= 0, messageFormat, messageArguments);
        }

        public static void ShouldBeEmpty(this string item)
        {
            ShouldBeEmpty(item, null, null);
        }

        public static void ShouldBeEmpty(this string item, string messageFormat, params object[] messageArguments)
        {
            Assert.AreEqual("", item, messageFormat, messageArguments);
        }

        public static void ShouldNotBeEmpty(this string item)
        {
            ShouldNotBeEmpty(item, null, null);
        }

        public static void ShouldNotBeEmpty(this string item, string messageFormat, params object[] messageArguments)
        {
            Assert.AreNotEqual("", item, messageFormat, messageArguments);
        }

        public static void ShouldNotBeNullOrEmpty(this string item)
        {
            Assert.IsFalse(string.IsNullOrEmpty(item), "Expected value that is not null and is not empty");
        }

        public static void ShouldBeGreaterThan(this IComparable left, IComparable right)
        {
            ShouldBeGreaterThan(left, right, null, null);
        }

        public static void ShouldBeGreaterThan(this IComparable left, IComparable right, string messageFormat, params object[] messageArguments)
        {
            Assert.Greater(left, right, messageFormat, messageArguments);
        }

        public static void ShouldBeGreaterThanOrEqualTo(this IComparable left, IComparable right)
        {
            ShouldBeGreaterThanOrEqualTo(left, right, null, null);
        }

        public static void ShouldBeGreaterThanOrEqualTo(this IComparable left, IComparable right, string messageFormat, params object[] messageArguments)
        {
            Assert.GreaterOrEqual(left, right, messageFormat, messageArguments);
        }

        public static void ShouldBeLessThan(this IComparable left, IComparable right)
        {
            ShouldBeLessThan(left, right, null, null);
        }

        public static void ShouldBeLessThan(this IComparable left, IComparable right, string messageFormat, params object[] messageArguments)
        {
            Assert.Less(left, right, messageFormat, messageArguments);
        }

        public static void ShouldBeLessThanOrEqualTo(this IComparable left, IComparable right)
        {
            ShouldBeLessThanOrEqualTo(left, right, null, null);
        }

        public static void ShouldBeLessThanOrEqualTo(this IComparable left, IComparable right, string messageFormat, params object[] messageArguments)
        {
            Assert.LessOrEqual(left, right, messageFormat, messageArguments);
        }

    }
}
