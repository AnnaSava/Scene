using AutoMapper;
using Framework.Base.DataService.Contract.Models.ListView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
