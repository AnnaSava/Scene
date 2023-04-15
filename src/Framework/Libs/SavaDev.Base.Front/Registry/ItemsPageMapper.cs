using AutoMapper;
using SavaDev.Base.Data.Registry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Front.Registry
{
    public class ItemsPageMapper
    {
        // TODO возможно, есть способ лучше, но пока так. По крайней мере проще будет отыскать вхождения
        public static ItemsPageViewModel<TViewModel> MapItems<TModel, TViewModel>(ItemsPage<TModel> itemsPage, IMapper mapper)
        {
            var vm = new ItemsPageViewModel<TViewModel>(
                itemsPage.Items.Select(m => mapper.Map<TModel, TViewModel>(m)),
                itemsPage.Page,
                itemsPage.TotalPages,
                itemsPage.TotalRows);

            return vm;
        }
    }
}
