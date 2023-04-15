using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace SavaDev.Base.Data.Registry
{
    public class ItemsPage<TModel>
    {
        public List<TModel> Items { get; }

        public int Page { get; }

        public int TotalPages { get; }

        public long TotalRows { get; }

        public ItemsPage(IPagedList<TModel> pagedList)
        {
            Items = pagedList.ToList();
            Page = pagedList.PageNumber;
            TotalPages = pagedList.PageCount;
            TotalRows = pagedList.TotalItemCount;
        }

        public ItemsPage(List<TModel> items, int page, int totalPages, long totalRows)
        {
            Items = items;
            Page = page;
            TotalPages = totalPages;
            TotalRows = totalRows;
        }
    }
}
