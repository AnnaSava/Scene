using SavaDev.Base.Data.Registry.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace SavaDev.Base.Data.Registry.Filter
{
    public static class IQueryableQueryExtentions
    {
        public static IQueryable<T> ApplyFilters<T>(this IQueryable<T> list, BaseFilter filter)
        {
            if (filter == null) return list;

            var filterType = filter.GetType();
            var filterFields = filterType.GetProperties();

            return list
                .ApplyStringFilters(filterFields.Where(f => f.PropertyType.Name == nameof(String)), filter)
                .ApplyBoolFilters(filterFields.Where(f => f.PropertyType.Name == nameof(Boolean)), filter)
                .ApplyWordFilters(filterFields.Where(f => f.PropertyType.Name == nameof(WordFilterField)), filter)
                .ApplyNullableBoolFilters(filterFields.Where(f => f.PropertyType.Name == "Nullable`1" && f.PropertyType.FullName.Contains("Boolean")), filter)
                .ApplyIEnumerableFilters(filterFields.Where(f => f.PropertyType.Name == "IEnumerable`1"), filter);
        }

        public static IQueryable<T> ApplySort<T>(this IQueryable<T> list, IEnumerable<RegistrySort> sorts)
        {
            var sort = sorts.FirstOrDefault();
            if (sort != null && !string.IsNullOrEmpty(sort.FieldName))
            {
                list = sort.Direction == SortDirection.Asc ? list.OrderBy(sort.FieldName) : list.OrderBy(sort.FieldName + " desc");
            };
            return list;
        }

        public static async Task<IPagedList<T>> ToPage<T>(this IQueryable<T> list, RegistryPageInfo pageInfo)
        {
            return await list.ToPagedListAsync(pageInfo.PageNumber, pageInfo.RowsCount);
        }

        private static IQueryable<T> ApplyStringFilters<T>(this IQueryable<T> list, IEnumerable<PropertyInfo> filterFields, BaseFilter filter)
        {
            if (filterFields == null) return list;

            foreach (var field in filterFields)
            {
                list = ApplyStringFilter(list, field.Name, field.GetValue(filter));
            }

            return list;
        }

        private static IQueryable<T> ApplyBoolFilters<T>(this IQueryable<T> list, IEnumerable<PropertyInfo> filterFields, BaseFilter filter)
        {
            if (filterFields == null) return list;

            foreach (var field in filterFields)
            {
                list = ApplyBoolFilter(list, field.Name, field.GetValue(filter));
            }

            return list;
        }
        private static IQueryable<T> ApplyNullableBoolFilters<T>(this IQueryable<T> list, IEnumerable<PropertyInfo> filterFields, BaseFilter filter)
        {
            if (filterFields == null) return list;

            foreach (var field in filterFields)
            {
                list = ApplyNullableBoolFilter(list, field.Name, field.GetValue(filter));
            }

            return list;
        }

        private static IQueryable<T> ApplyWordFilters<T>(this IQueryable<T> list, IEnumerable<PropertyInfo> filterFields, BaseFilter filter)
        {
            if (filterFields == null) return list;

            foreach (var field in filterFields)
            {
                list = ApplyWordFilter(list, field.Name, field.GetValue(filter));
            }

            return list;
        }

        private static IQueryable<T> ApplyIEnumerableFilters<T>(this IQueryable<T> list, IEnumerable<PropertyInfo> filterFields, BaseFilter filter)
        {
            if (filterFields == null) return list;

            foreach (var field in filterFields)
            {
                list = ApplyIEnumerableFilter(list, field.Name, field.GetValue(filter));
            }
            return list;
        }

        private static IQueryable<T> ApplyStringFilter<T>(IQueryable<T> list, string fieldName, object fieldObj)
        {
            if (fieldObj == null) return list;

            var field = (string)fieldObj;

            var str = string.Format("{0} == {1}", fieldName, field);
            list = list.Where(str);

            return list;
        }

        private static IQueryable<T> ApplyBoolFilter<T>(IQueryable<T> list, string fieldName, object fieldObj)
        {
            var field = (bool)fieldObj;

            var str = string.Format("{0} == {1}", fieldName, field);
            list = list.Where(str);

            return list;
        }

        private static IQueryable<T> ApplyNullableBoolFilter<T>(IQueryable<T> list, string fieldName, object fieldObj)
        {
            var field = fieldObj as bool?;
            if (field == null) return list;

            string str;
            if (field.HasValue)
            {
                str = string.Format("{0} == {1}", fieldName, field.Value);
                list = list.Where(str);
            }

            return list;
        }

        private static IQueryable<T> ApplyWordFilter<T>(IQueryable<T> list, string fieldName, object fieldObj)
        {
            var field = fieldObj as WordFilterField;
            if (field == null) return list;

            string str;

            switch (field.MatchMode)
            {
                case MatchModeWord.Equals:
                    str = string.Format("{0} == \"{1}\"", fieldName, field.Value.FirstOrDefault());
                    list = list.Where(str);
                    break;
                case MatchModeWord.NotEquals:
                    str = string.Format("{0} != \"{1}\"", fieldName, field.Value.FirstOrDefault());
                    list = list.Where(str);
                    break;
                case MatchModeWord.StartsWith:
                    str = string.Format("{0}.StartsWith(@0)", fieldName);
                    list = list.Where(str, field.Value.FirstOrDefault());
                    break;
                case MatchModeWord.EndsWith:
                    str = string.Format("{0}.EndsWith(@0)", fieldName);
                    list = list.Where(str, field.Value.FirstOrDefault());
                    break;
                case MatchModeWord.Contains:
                    str = string.Format("{0}.Contains(@0)", fieldName);
                    list = list.Where(str, field.Value.FirstOrDefault());
                    break;
                case MatchModeWord.NotContains:
                    str = string.Format("not ({0}.Contains(@0))", fieldName);
                    list = list.Where(str, field.Value.FirstOrDefault());
                    break;
                case MatchModeWord.In:
                    str = string.Format("it.{0} in @0", fieldName);
                    list = list.Where(str, field.Value);
                    break;
                case MatchModeWord.NotIn:
                    str = string.Format("not (it.{0} in @0)", fieldName);
                    list = list.Where(str, field.Value);
                    break;
            }

            return list;
        }

        private static IQueryable<T> ApplyIEnumerableFilter<T>(IQueryable<T> list, string fieldName, object fieldObj)
        {
            if (fieldObj == null) return list;

            var str = string.Format("it.{0} in @0", fieldName);
            list = list.Where(str, fieldObj);

            return list;
        }
    }
}
