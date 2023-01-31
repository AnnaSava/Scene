using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Framework.Base.Types.Registry
{
    [Obsolete]
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
    }
}
