using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Xunit;

namespace OpenPGPTestingHelpers
{
    /// <summary>
    /// Creates extension methods that make it easier to write assertions.
    /// </summary>
    public static class AssertionExtensions
    {
        public static void ShouldBe<T>(this T item, T expectedValue)
        {
            Assert.Equal(expectedValue, item);
        }

        public static void ShouldNotBe<T>(this T item, T expectedValue)
        {
            Assert.NotEqual(expectedValue, item);
        }

        public static void ShouldBeSameReference<T>(this T item, T expectedValue) where T : class
        {
            Assert.Same(expectedValue, item);
        }

        public static void ShouldNotBeSameReference<T>(this T item, T expectedValue) where T : class
        {
            Assert.NotSame(expectedValue, item);
        }

        public static void ShouldBeTrue(this bool item)
        {
            Assert.True(item);
        }

        public static void ShouldBeFalse(this bool item)
        {
            Assert.False(item);
        }

        public static void ShouldBeNull(this object item)
        {
            Assert.Null(item);
        }

        public static void ShouldNotBeNull(this object item)
        {
            Assert.NotNull(item);
        }

        public static void ShouldContain(this string item, string expectedSubString)
        {
            Assert.Contains(expectedSubString, item);
        }

        public static void ShouldNotContain(this string item, string unexpectedSubString)
        {
            Assert.DoesNotContain(unexpectedSubString, item);
        }

        public static void ShouldBeCaseInsensitive(this string item, string expectedValue)
        {
            Assert.Equal(expectedValue, item, new CaseInsensitiveEqualityComparer());
        }

        public static void ShouldNotBeCaseInsensitive(this string item, string expectedValue)
        {
            Assert.NotEqual(expectedValue, item, new CaseInsensitiveEqualityComparer());
        }

        public static void ShouldBeEmpty(this string item)
        {
            Assert.Equal("", item);
        }

        public static void ShouldNotBeEmpty(this string item)
        {
            Assert.NotEqual("", item);
        }

        public static void ShouldNotBeNullOrEmpty(this string item)
        {
            Assert.False(string.IsNullOrEmpty(item), "Expected value that is not null and is not empty");
        }

        public static void ShouldNotBeNullEmptyOrWhiteSpace(this string item)
        {
            Assert.False(string.IsNullOrWhiteSpace(item), "Expected value that is not null and is not empty and is not whitespace");
        }

        public static void ShouldStartWith(this string item, string expectedText)
        {
            Assert.True(item.StartsWith(expectedText));
        }

        public static void ShouldEndWith(this string item, string expectedText)
        {
            Assert.True(item.EndsWith(expectedText));
        }

        public static void ShouldContain<T>(this IEnumerable<T> item, T expectedValue)
        {
            Assert.Contains(expectedValue, item);
        }

        public static void ShouldNotContain<T>(this IEnumerable<T> item, T expectedValue)
        {
            Assert.DoesNotContain(expectedValue, item);
        }

        public static void ShouldBeEmpty(this IEnumerable item)
        {
            Assert.Empty(item);
        }

        public static void ShouldNotBeEmpty(this IEnumerable item)
        {
            Assert.NotEmpty(item);
        }

        public static void ShouldBeGreaterThan<T>(this T item, T right) where T : IComparable<T>
        {
            Assert.True(item.CompareTo(right) > 0);
        }

        public static void ShouldBeGreaterThanOrEqualTo<T>(this T item, T right) where T : IComparable<T>
        {
            Assert.True(item.CompareTo(right) >= 0);
        }

        public static void ShouldBeLessThan<T>(this T item, T right) where T : IComparable<T>
        {
            Assert.True(item.CompareTo(right) < 0);
        }

        public static void ShouldBeLessThanOrEqualTo<T>(this T item, T right) where T : IComparable<T>
        {
            Assert.True(item.CompareTo(right) <= 0);
        }

        public static void ShouldBeBetween<T>(this T item, T minimum, T maximum) where T : IComparable
        {
            Assert.InRange(item, minimum, maximum);
        }

        public static void ShouldNotBeBetween<T>(this T item, T minimum, T maximum) where T : IComparable
        {
            Assert.NotInRange(item, minimum, maximum);
        }
    }
}
