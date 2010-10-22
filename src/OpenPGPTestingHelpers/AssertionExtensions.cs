using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MbUnit.Framework;

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
            Assert.Contains(item, expectedSubString, messageFormat, messageArguments);
        }

        public static void ShouldNotContain(this string item, string unexpectedSubString, string messageFormat = null, params object[] messageArguments)
        {
            Assert.DoesNotContain(item, unexpectedSubString, messageFormat, messageArguments);
        }

        public static void ShouldBeCaseInsensitive(this string item, string expectedValue, string messageFormat = null, params object[] messageArguments)
        {
            Assert.AreEqual(expectedValue, item, StringComparison.InvariantCultureIgnoreCase, messageFormat, messageArguments);
        }

        public static void ShouldNotBeCaseInsensitive(this string item, string expectedValue, string messageFormat = null, params object[] messageArguments)
        {
            Assert.AreEqual(expectedValue, item, StringComparison.InvariantCultureIgnoreCase, messageFormat, messageArguments);
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

        public static void ShouldNotBeNullEmptyOrWhiteSpace(this string item)
        {
            Assert.IsFalse(string.IsNullOrWhiteSpace(item), "Expected value that is not null and is not empty and is not whitespace");
        }

        public static void ShouldMatchRegex(this string item, string regexPattern, string messageFormat = null, params object[] messageArguments)
        {
            Assert.FullMatch(item, regexPattern, messageFormat, messageArguments);
        }

        public static void ShouldMatchRegex(this string item, string regexPattern, RegexOptions regexOptions, string messageFormat = null, params object[] messageArguments)
        {
            Assert.FullMatch(item, regexPattern, regexOptions, messageFormat, messageArguments);
        }

        public static void ShouldBeLikeRegex(this string item, string regexPattern, string messageFormat = null, params object[] messageArguments)
        {
            Assert.Like(item, regexPattern, messageFormat, messageArguments);
        }

        public static void ShouldBeLikeRegex(this string item, string regexPattern, RegexOptions regexOptions, string messageFormat = null, params object[] messageArguments)
        {
            Assert.Like(item, regexPattern, regexOptions, messageFormat, messageArguments);
        }

        public static void ShouldNotBeLikeRegex(this string item, string regexPattern, string messageFormat = null, params object[] messageArguments)
        {
            Assert.NotLike(item, regexPattern, messageFormat, messageArguments);
        }

        public static void ShouldNotBeLikeRegex(this string item, string regexPattern, RegexOptions regexOptions, string messageFormat = null, params object[] messageArguments)
        {
            Assert.NotLike(item, regexPattern, regexOptions, messageFormat, messageArguments);
        }

        public static void ShouldStartWith(this string item, string expectedText, string messageFormat = null, params object[] messageArguments)
        {
            Assert.StartsWith(item, expectedText, messageFormat, messageArguments);
        }

        public static void ShouldEndWith(this string item, string expectedText, string messageFormat = null, params object[] messageArguments)
        {
            Assert.EndsWith(item, expectedText, messageFormat, messageArguments);
        }

        public static void ShouldContain<T>(this IEnumerable<T> item, T expectedValue, string messageFormat = null, params object[] messageArguments)
        {
            Assert.Contains(item, expectedValue, messageFormat, messageArguments);
        }

        public static void ShouldNotContain<T>(this IEnumerable<T> item, T expectedValue, string messageFormat = null, params object[] messageArguments)
        {
            Assert.DoesNotContain(item, expectedValue, messageFormat, messageArguments);
        }

        public static void ShouldContainKey<TKey, TValue>(this IDictionary<TKey, TValue> item, TKey expectedKey, string messageFormat = null, params object[] messageArguments)
        {
            Assert.ContainsKey(item, expectedKey, messageFormat, messageArguments);
        }

        public static void ShouldNotContainKey<TKey, TValue>(this IDictionary<TKey, TValue> item, TKey expectedKey, string messageFormat = null, params object[] messageArguments)
        {
            Assert.DoesNotContainKey(item, expectedKey, messageFormat, messageArguments);
        }

        public static void ShouldBeEmpty(this IEnumerable item, string messageFormat = null, params object[] messageArguments)
        {
            Assert.IsEmpty(item, messageFormat, messageArguments);
        }

        public static void ShouldNotBeEmpty(this IEnumerable item, string messageFormat = null, params object[] messageArguments)
        {
            Assert.IsNotEmpty(item, messageFormat, messageArguments);
        }

        public static void ShouldBeGreaterThan<T>(this T item, T right, string messageFormat = null, params object[] messageArguments)
        {
            Assert.GreaterThan(item, right, messageFormat, messageArguments);
        }

        public static void ShouldBeGreaterThanOrEqualTo<T>(this T item, T right, string messageFormat = null, params object[] messageArguments)
        {
            Assert.GreaterThanOrEqualTo(item, right, messageFormat, messageArguments);
        }

        public static void ShouldBeLessThan<T>(this T item, T right, string messageFormat = null, params object[] messageArguments)
        {
            Assert.LessThan(item, right, messageFormat, messageArguments);
        }

        public static void ShouldBeLessThanOrEqualTo<T>(this T item, T right, string messageFormat = null, params object[] messageArguments)
        {
            Assert.LessThanOrEqualTo(item, right, messageFormat, messageArguments);
        }

        public static void ShouldBeBetween<T>(this T item, T minimum, T maximum, string messageFormat = null, params object[] messageArguments)
        {
            Assert.Between(item, minimum, maximum, messageFormat, messageArguments);
        }

        public static void ShouldNotBeBetween<T>(this T item, T minimum, T maximum, string messageFormat = null, params object[] messageArguments)
        {
            Assert.NotBetween(item, minimum, maximum, messageFormat, messageArguments);
        }
    }
}
