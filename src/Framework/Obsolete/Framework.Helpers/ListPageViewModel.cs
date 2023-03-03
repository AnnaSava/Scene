using System;
using System.Collections.Generic;

namespace Framework.Base.Service.ListView
{
    [Obsolete]
    public class ListPageViewModel<T>
    {
        public List<T> Items { get; set; }

        public int Page { get; set; }

        public int TotalPages { get; set; }

        public long TotalRows { get; set; }
    }
}
