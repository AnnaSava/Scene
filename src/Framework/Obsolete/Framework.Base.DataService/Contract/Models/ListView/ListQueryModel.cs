using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Base.DataService.Contract.Models
{
    public class ListQueryModel<TFilter> where TFilter : IFilter, new()
    {
        public TFilter Filter { get; set; } = new TFilter();

        public PageInfoModel PageInfo { get; set; } = new PageInfoModel { PageNumber = 1, RowsCount = 10 };

        public ListQueryModel() { }

        public ListQueryModel(TFilter filter, PageInfoModel pageInfo)
        {
            Filter = filter;
            PageInfo = pageInfo;
        }

        public ListQueryModel(TFilter filter, int page, int count)
        {
            Filter = filter;
            PageInfo = new PageInfoModel { PageNumber = page, RowsCount = count };
        }
    }
}
