using SavaDev.Base.Data.Registry.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Data.Registry.Filter
{
    public static class FieldMappingStringExtentions
    {
        const char SortArraySeparator = ',';
        const char DescSortSign = '-';
        const char FilterPropsSeparator = '|';
        public static Dictionary<string, SortDirection> ToSortDictionary(this string inputString)
        {
            if (string.IsNullOrEmpty(inputString))
                return null;

            var dictionary = new Dictionary<string, SortDirection>();

            var stringsArr = inputString.Split(SortArraySeparator);
            foreach (var str in stringsArr)
            {
                var tstr = str.Trim();
                if (tstr.First() == DescSortSign)
                {
                    dictionary.Add(tstr[1..], SortDirection.Desc);
                }
                else
                {
                    dictionary.Add(tstr, SortDirection.Asc);
                }
            }
            return dictionary;
        }

        public static NumericFilterField<long> ToLongListFilterField(this string filterField)
        {
            if (string.IsNullOrEmpty(filterField))
                return null;

            var fieldPropsArr = filterField.Split(FilterPropsSeparator);

            if (fieldPropsArr.Length == 0)
                return null;

            //Если в массиве один элемент, то предполагаем, что это искомое значение. Выставляем по умолчанию режим сравнения равенство.
            if (fieldPropsArr.Length == 1)
            {
                return new NumericFilterField<long>()
                {
                    MatchMode = MatchModeNumeric.Equals,
                    Value = fieldPropsArr[0].ToLongList()
                };
            }

            //Разбираем первый и второй элементы. Если элементов больше, то просто игнорируем их.
            if (Enum.TryParse(typeof(MatchMode), fieldPropsArr[0], ignoreCase: true, out object matchMode))
            {
                return new NumericFilterField<long>()
                {
                    MatchMode = (MatchModeNumeric)matchMode,
                    Value = fieldPropsArr[1].ToLongList()
                };
            }

            return null;
        }

        public static WordFilterField ToWordListFilterField(this string filterField)
        {
            if (string.IsNullOrEmpty(filterField))
                return null;

            var fieldPropsArr = filterField.Split(FilterPropsSeparator);

            if (fieldPropsArr.Length == 0)
                return null;

            //Если в массиве один элемент, то предполагаем, что это искомое значение. Выставляем по умолчанию режим сравнения равенство.
            if (fieldPropsArr.Length == 1)
            {
                return new WordFilterField()
                {
                    MatchMode = MatchModeWord.Equals,
                    Value = fieldPropsArr[0].ToStringList()
                };
            }

            //Разбираем первый и второй элементы. Если элементов больше, то просто игнорируем их.
            if (Enum.TryParse(typeof(MatchModeWord), fieldPropsArr[0], ignoreCase: true, out object matchMode))
            {
                return new WordFilterField()
                {
                    MatchMode = (MatchModeWord)matchMode,
                    Value = fieldPropsArr[1].ToStringList()
                };
            }

            return null;
        }

        public static TextFilterField ToTextFilterField(this string filterField)
        {
            if (string.IsNullOrEmpty(filterField))
                return null;

            var fieldPropsArr = filterField.Split(FilterPropsSeparator);

            if (fieldPropsArr.Length == 0)
                return null;

            //Если в массиве один элемент, то предполагаем, что это искомое значение. Выставляем по умолчанию режим сравнения равенство.
            if (fieldPropsArr.Length == 1)
            {
                return new TextFilterField()
                {
                    MatchMode = MatchModeText.Contains,
                    Value = fieldPropsArr[0]
                };
            }

            //Разбираем первый и второй элементы. Если элементов больше, то просто игнорируем их.
            if (Enum.TryParse(typeof(MatchModeText), fieldPropsArr[0], ignoreCase: true, out object matchMode))
            {
                return new TextFilterField()
                {
                    MatchMode = (MatchModeText)matchMode,
                    Value = fieldPropsArr[1]
                };
            }

            return null;
        }
    }
}

