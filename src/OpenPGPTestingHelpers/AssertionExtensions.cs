using System;
using NUnit.Framework;

namespace OpenPGPTestingHelpers
{
    /// <summary>
    /// Creates extension methods that make it easier to write assertions.
    /// </summary>
    public static class AssertionExtensions
    {
        public static void ShouldBe<T>(this T item, T expectedValue, string messageFormat = null, params object[] messageArguments)
        {
            Assert.AreEqual(expectedValue, item, messageFormat, messageArguments);
        }

        public static void ShouldNotBe<T>(this T item, T expectedValue, string messageFormat = null, params object[] messageArguments)
        {
            Assert.AreNotEqual(expectedValue, item, messageFormat, messageArguments);
        }

        public static void ShouldBeSameReference<T>(this T item, T expectedValue, string messageFormat = null, params object[] messageArguments) where T : class
        {
            Assert.AreSame(expectedValue, item, messageFormat, messageArguments);
        }

        public static void ShouldNotBeSameReference<T>(this T item, T expectedValue, string messageFormat = null, params object[] messageArguments) where T : class
        {
            Assert.AreNotSame(expectedValue, item, messageFormat, messageArguments);
        }

        public static void ShouldBeTrue(this bool item, string messageFormat = null, params object[] messageArguments)
        {
            Assert.IsTrue(item, messageFormat, messageArguments);
        }

        public static void ShouldBeFalse(this bool item, string messageFormat = null, params object[] messageArguments)
        {
            Assert.IsFalse(item, messageFormat, messageArguments);
        }

        public static void ShouldBeNull(this object item, string messageFormat = null, params object[] messageArguments)
        {
            Assert.IsNull(item, messageFormat, messageArguments);
        }

        public static void ShouldNotBeNull(this object item, string messageFormat = null, params object[] messageArguments)
        {
            Assert.IsNotNull(item, messageFormat, messageArguments);
        }

        public static void ShouldContain(this string item, string expectedSubString, string messageFormat = null, params object[] messageArguments)
        {
            Assert.IsTrue(item.IndexOf(expectedSubString) >= 0, messageFormat, messageArguments);
        }

        public static void ShouldNotContain(this string item, string unexpectedSubString, string messageFormat = null, params object[] messageArguments)
        {
            Assert.IsFalse(item.IndexOf(unexpectedSubString) >= 0, messageFormat, messageArguments);
        }

        public static void ShouldBeEmpty(this string item, string messageFormat = null, params object[] messageArguments)
        {
            Assert.AreEqual("", item, messageFormat, messageArguments);
        }

        public static void ShouldNotBeEmpty(this string item, string messageFormat = null, params object[] messageArguments)
        {
            Assert.AreNotEqual("", item, messageFormat, messageArguments);
        }

        public static void ShouldNotBeNullOrEmpty(this string item)
        {
            Assert.IsFalse(string.IsNullOrEmpty(item), "Expected value that is not null and is not empty");
        }

        public static void ShouldBeGreaterThan(this IComparable left, IComparable right, string messageFormat = null, params object[] messageArguments)
        {
            Assert.Greater(left, right, messageFormat, messageArguments);
        }

        public static void ShouldBeGreaterThanOrEqualTo(this IComparable left, IComparable right, string messageFormat = null, params object[] messageArguments)
        {
            Assert.GreaterOrEqual(left, right, messageFormat, messageArguments);
        }

        public static void ShouldBeLessThan(this IComparable left, IComparable right, string messageFormat = null, params object[] messageArguments)
        {
            Assert.Less(left, right, messageFormat, messageArguments);
        }

        public static void ShouldBeLessThanOrEqualTo(this IComparable left, IComparable right, string messageFormat = null, params object[] messageArguments)
        {
            Assert.LessOrEqual(left, right, messageFormat, messageArguments);
        }

    }
}
