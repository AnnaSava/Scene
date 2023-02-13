using SavaDev.Base.Data.Registry.Enums;
using SavaDev.Base.Data.Registry.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Data.Registry
{
    public class RegistryQuery<TFilter> : RegistryQuery
    {
        [Obsolete]
        public TFilter Filter { get; set; }

        public RegistryQuery(RegistryPageInfo pageInfo, string sortString) : base(pageInfo, sortString)
        {
        }

        public RegistryQuery(RegistryPageInfo pageInfo, List<RegistrySort> sort) : base(pageInfo, sort)
        {
        }

        public RegistryQuery(RegistryPageInfo pageInfo, RegistrySort sort) : base(pageInfo, sort)
        {
        }

    }

    public class RegistryQuery
    {
        public BaseFilter Filter0 { get; set; }

        public RegistryPageInfo PageInfo { get; set; } = new RegistryPageInfo();

        public List<RegistrySort> Sort { get; private set; } = new List<RegistrySort>();

        public RegistryQuery() { }

        public RegistryQuery(int pageNumber, int rowsCount)
        {
            PageInfo.PageNumber = pageNumber;
            PageInfo.RowsCount = rowsCount;
        }

        public RegistryQuery(int pageNumber, int rowsCount, string sortString)
        {
            PageInfo.PageNumber = pageNumber;
            PageInfo.RowsCount = rowsCount;
            SetNewSort(ParseSortString(sortString));
        }

        public RegistryQuery(RegistryPageInfo pageInfo, RegistrySort sort)
        {
            PageInfo = pageInfo;
            SetNewSort(sort);
        }

        public RegistryQuery(int pageNumber, int rowsCount, List<RegistrySort> sort)
        {
            PageInfo.PageNumber = pageNumber;
            PageInfo.RowsCount = rowsCount;
            SetNewSort(sort);
        }

        public RegistryQuery(RegistryPageInfo pageInfo, string sortString)
        {
            PageInfo = pageInfo;
            SetNewSort(ParseSortString(sortString));
        }

        public RegistryQuery(RegistryPageInfo pageInfo, List<RegistrySort> sort)
        {
            PageInfo = pageInfo;
            SetNewSort(sort);
        }

        public void SetNewSort(RegistrySort sort)
        {
            Sort.Clear();
            Sort.Add(sort);
        }

        public void SetNewSort(IEnumerable<RegistrySort> sort)
        {
            Sort.Clear();
            Sort.AddRange(sort);
        }

        private static List<RegistrySort> ParseSortString(string sortString)
        {
            if (string.IsNullOrEmpty(sortString)) return null;

            var arr = sortString.Split('_');

            var sorts = new List<RegistrySort>();
            foreach (var item in arr)
            {
                RegistrySort sort;
                if (item.EndsWith("Desc"))
                {
                    sort = new RegistrySort(item.Replace("Desc", ""), SortDirection.Desc);
                }
                else
                {
                    sort = new RegistrySort(item);
                }
                sorts.Add(sort);
            }
            return sorts;
        }
    }
}
