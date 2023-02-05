using SavaDev.Base.Data.Registry;
using SavaDev.Base.Data.Registry.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Data.Services
{
    public interface IEntityRegistryService
    {

    }

    public interface IEntityRegistryService<TItemsModel, TFilterModel>
        where TFilterModel : BaseFilter
    {
        Task<RegistryPage<TItemsModel>> GetRegistryPage(RegistryQuery<TFilterModel> query);
    }
}
