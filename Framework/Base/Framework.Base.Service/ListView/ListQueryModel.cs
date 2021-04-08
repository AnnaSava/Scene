using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Base.Service.ListView.Models
{
    public class ListQueryModel<TFilter> where TFilter : ListItemsFilterModel
    {
        public TFilter Filter { get; set; }

        public IEnumerable<ListItemsSortOrder> Sort { get; set; }

        public int Page { get; set; }

        public int Count { get; set; }

        public ListItemsDisplay Display { get; set; }
    }
}
