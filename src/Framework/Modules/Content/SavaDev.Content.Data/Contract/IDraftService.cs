using Savadev.Content.Data.Contract.Models;
using SavaDev.Base.Data.Registry;
using SavaDev.Base.Data.Services;
using SavaDev.Base.Front.Registry;
using SavaDev.Content.Data.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savadev.Content.Data.Contract
{
    public interface IDraftService
    {
        Task<OperationResult<DraftModel>> Create<T>(DraftModel model, T contentModel);

        Task<OperationResult> Update<T>(Guid id, T contentModel);

        Task<OperationResult> SetContentId(Guid id, string contentId);

        Task<OperationResult> Delete(Guid id);

        Task<DraftModel> GetOne(Guid id);

        Task<ItemsPage<DraftModel>> GetAll(RegistryQuery<DraftStrictFilterModel> query);

        Task<ItemsPage<DraftModel>> GetAll(RegistryQuery<DraftFilterModel> query);
    }
}
