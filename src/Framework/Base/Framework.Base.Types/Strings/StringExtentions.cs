using Framework.Base.Types.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Helpers.TypeHelpers
{
    public enum SequenceType
    {
        Enumeration,
        Interval,
        EnumerationInterval
    }

    public static class StringExtentions
    {
        const char DefaultArraySeparator = ',';
        const int FragmentLength = 50;
        const string FragmentEnding = "...";

        public static List<int> ToIntList(this string inputString, char separator = DefaultArraySeparator)
        {
            return inputString.ToParsedList<int>(int.TryParse);
        }

        public static List<long> ToLongList(this string inputString, char separator = DefaultArraySeparator)
        {
            return inputString.ToParsedList<long>(long.TryParse);
        }

        public static List<T> ToParsedList<T>(this string inputString, TryParseHandler<T> handler, char separator = DefaultArraySeparator) where T : struct
        {
            if (string.IsNullOrEmpty(inputString))
                return new List<T> { };

            var stringsArr = inputString.Split(separator);
            return stringsArr.Select(m => handler(m.Trim(), out T res) ? res : (T?)null)
                .Where(m => m != null)
                .Select(m => (T)m)
                .ToList();
        }

        public delegate bool TryParseHandler<T>(string value, out T result);

        public static List<long> ToLongList(this string inputString, SequenceType sequenceType, char separator = DefaultArraySeparator)
        {
            var result = new List<long>();

            if (string.IsNullOrEmpty(inputString))
                return result;

            switch (sequenceType)
            {
                case SequenceType.Enumeration:
                    var stringsArr = inputString.Split(separator);
                    return stringsArr.Select(m => long.TryParse(m.Trim(), out long res) ? res : (long?)null)
                        .Where(m => m != null)
                        .Select(m => (long)m)
                        .ToList();
                case SequenceType.Interval:
                    FillRange(inputString);
                    return result;
                case SequenceType.EnumerationInterval:
                    var arr = inputString.Split(separator);
                    foreach (var item in arr)
                    {
                        FillRange(item);
                    }
                    return result.Distinct().OrderBy(m => m).ToList();
                default: return new List<long>();
            }

            void FillRange(string rangeString)
            {
                var rangeArr = rangeString.Split('-');
                if (rangeArr.Length == 1 && long.TryParse(rangeArr[0].Trim(), out long res))
                {
                    result.Add(res);
                }
                if (rangeArr.Length != 1 && rangeArr.Length != 2) return;

                var parsed = rangeArr.Select(m => long.TryParse(m.Trim(), out long res) ? res : (long?)null)
                         .Where(m => m != null)
                         .Select(m => (long)m)
                         .OrderBy(m => m);

                if (parsed.Count() == 0) return;

                if (parsed.Last() - parsed.First() > 1000)
                    result.AddRange(parsed);
                else
                {
                    for (var i = parsed.First(); i <= parsed.Last(); i++)
                        result.Add(i);
                }
            }
        }

        public static List<string> ToStringList(this string inputString, char separator = DefaultArraySeparator)
        {
            if (string.IsNullOrEmpty(inputString))
                return new List<string> { };

            return inputString.Split(separator).ToList();
        }

        public static string Fragment(this string inputString, int length = FragmentLength, string ending = FragmentEnding)
        {
            return inputString.Substring(0, length) + ending;
        }

        public static string ToSnakeCase(this string inputString)
        {
            if (inputString == null)
            {
                throw new ArgumentNullException(nameof(inputString));
            }
            if (inputString.Length < 2)
            {
                return inputString;
            }
            var sb = new StringBuilder();
            sb.Append(char.ToLowerInvariant(inputString[0]));
            for (int i = 1; i < inputString.Length; ++i)
            {
                char c = inputString[i];
                if (char.IsUpper(c))
                {
                    sb.Append('_');
                    sb.Append(char.ToLowerInvariant(c));
                }
                else
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
    }
}
