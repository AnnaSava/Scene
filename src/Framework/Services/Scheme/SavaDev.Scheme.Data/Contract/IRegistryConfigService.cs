using SavaDev.Base.Data.Services;
using SavaDev.Scheme.Data.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Scheme.Data.Contract
{
    public interface IRegistryConfigService
    {
        Task<OperationResult> Create(RegistryConfigModel model);

        Task<OperationResult> Update(long id, RegistryConfigModel model);

        Task<OperationResult> Remove(long id);

        Task<RegistryConfigModel> GetLast(Guid tableId);

        Task<RegistryConfigModel> GetOne(long id);

        Task<IEnumerable<RegistryConfigModel>> GetAll(Guid tableId);
    }
}
