using SavaDev.Base.Data.Models;
using SavaDev.Base.Data.Registry.Filter;
using SavaDev.Base.Data.Services;
using SavaDev.Base.Front.Services;
using SavaDev.Scheme.Front.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Scheme.Front.Contract
{
    public interface IRegistryViewService
    {
        Task<RegistryViewModel> GetOne(ModelPlacement placement);

        Task<List<FilterViewModel>> GetAllFilters(Guid registryId);

        Task ApplyFilter(long filterId, RegistryViewModel vm);

        Task ApplyConfig(long configId, RegistryViewModel vm);

        Task<OperationViewResult> SaveFilter(FilterViewModel model, BaseFilter filter);

        Task<OperationViewResult> RemoveFilter(long id);

        Task<OperationViewResult> SaveConfig(RegistryConfigViewModel model);

        Task<OperationViewResult> RemoveConfig(long id);

        Task<RegistryConfigViewModel> GetLastConfig(Guid tableId);
    }
}
