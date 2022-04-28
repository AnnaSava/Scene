using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Base.Service.ListView
{
    public class ListPageViewModel<T>
    {
        public IEnumerable<T> Items { get; set; }

        public int Page { get; set; }

        public int TotalPages { get; set; }

        public long TotalRows { get; set; }
    }
}
