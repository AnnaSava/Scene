using AutoMapper;
using SavaDev.Base.Data.Registry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Front.Registry
{
    public class RegistryPageMapper
    {
        // TODO возможно, есть способ лучше, но пока так. По крайней мере проще будет отыскать вхождения
        public static RegistryPageViewModel<TViewModel> MapRegistry<TModel, TViewModel>(RegistryPage<TModel> registryPage, IMapper mapper)
        {
            var vm = new RegistryPageViewModel<TViewModel>(
                registryPage.Items.Select(m => mapper.Map<TModel, TViewModel>(m)),
                registryPage.Page,
                registryPage.TotalPages,
                registryPage.TotalRows);

            return vm;
        }

        public static ItemsPageViewModel<TViewModel> MapItems<TModel, TViewModel>(ItemsPage<TModel> registryPage, IMapper mapper)
        {
            var vm = new ItemsPageViewModel<TViewModel>(
                registryPage.Items.Select(m => mapper.Map<TModel, TViewModel>(m)),
                registryPage.Page,
                registryPage.TotalPages,
                registryPage.TotalRows);

            return vm;
        }
    }
}
