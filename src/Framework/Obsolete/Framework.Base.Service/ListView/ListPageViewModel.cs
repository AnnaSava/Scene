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

    [Obsolete]
    public class ListPageViewModel
    {
        // TODO возможно, есть способ лучше, но пока так. По крайней мере проще будет отыскать вхождения
        public static ListPageViewModel<TViewModel> Map<TModel, TViewModel>(PageListModel<TModel> pageList, IMapper mapper)
        {
            var vm = new ListPageViewModel<TViewModel>()
            {
                Items = pageList.Items.Select(m => mapper.Map<TModel, TViewModel>(m)).ToList(),
                Page = pageList.Page,
                TotalPages = pageList.TotalPages,
                TotalRows = pageList.TotalRows
            };

            return vm;
        }
    }
}
