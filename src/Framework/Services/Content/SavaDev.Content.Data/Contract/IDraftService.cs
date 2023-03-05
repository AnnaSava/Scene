using SavaDev.Base.Data.Registry;
using SavaDev.Base.Data.Services;
using SavaDev.Content.Data.Contract.Models;

namespace SavaDev.Content.Data.Contract
{
    public interface IDraftService
    {
        Task<OperationResult> Create<T>(DraftModel model, T contentModel);

        Task<OperationResult> Update<T>(Guid id, T contentModel);

        Task<OperationResult> SetContentId(Guid id, string contentId);

        Task<OperationResult> Delete(Guid id);

        Task<DraftModel> GetOne(Guid id);

        Task<RegistryPage<DraftModel>> GetRegistryPage(RegistryQuery<DraftFilterModel> query);

        Task<ItemsPage<DraftModel>> GetAll(RegistryQuery query);
    }
}
