using Framework.Helpers.TypeHelpers;
using System;
using System.Collections.Generic;
using Xunit;

namespace Framework.Helpers.Tests
{
    public class StringTests
    {
        [Theory]
        [MemberData(nameof(EnumerationData))]
        public void ToLongList_SimpleEnumeration(string inputStr, long[] expected)
        {
            // Arrange

            // Act
            var result = inputStr.ToParsedList<long>(long.TryParse);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [MemberData(nameof(EnumerationData))]
        public void ToLongList_Enumeration(string inputStr, long[] expected)
        {
            // Arrange

            // Act
            var result = inputStr.ToLongList(SequenceType.Enumeration);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [MemberData(nameof(IntervalData))]
        public void ToLongList_Interval(string inputStr, long[] expected)
        {
            // Arrange

            // Act
            var result = inputStr.ToLongList(SequenceType.Interval);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [MemberData(nameof(EnumerationIntervalData))]
        public void ToLongList_EnumerationInterval(string inputStr, long[] expected)
        {
            // Arrange

            // Act
            var result = inputStr.ToLongList(SequenceType.EnumerationInterval);

            // Assert
            Assert.Equal(expected, result);
        }

        public static IEnumerable<object[]> EnumerationData => new List<object[]>
        {
            new object[] { "1,3,5,6,7", new long[] { 1, 3, 5, 6, 7 } },
            new object[] { "1,3,5,,6,7", new long[] { 1, 3, 5, 6, 7 } },
            new object[] { "1,3,5,zx,6,7", new long[] { 1, 3, 5, 6, 7 } },
            new object[] { "1,  3,5 ,6,7", new long[] { 1, 3, 5, 6, 7 } },
        };

        public static IEnumerable<object[]> IntervalData => new List<object[]>
        {
            new object[] { "3-7", new long[] { 3, 4, 5, 6, 7 } },
            new object[] { "7-3", new long[] { 3, 4, 5, 6, 7 } },
            new object[] { "3 -7", new long[] { 3, 4, 5, 6, 7 } },
            new object[] { " 3- 7", new long[] { 3, 4, 5, 6, 7 } },
        };

        public static IEnumerable<object[]> EnumerationIntervalData => new List<object[]>
        {
            new object[] { "1,3,5,6,7", new long[] { 1, 3, 5, 6, 7 } },
            new object[] { "3-7", new long[] { 3, 4, 5, 6, 7 } },
            new object[] { "1-3,6,9", new long[] { 1, 2, 3, 6, 9 } },
            new object[] { "2-5,8-10", new long[] { 2, 3, 4, 5, 8, 9, 10 } },
        };
    }
}
