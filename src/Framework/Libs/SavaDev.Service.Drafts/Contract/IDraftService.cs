using SavaDev.Base.Data.Registry;
using SavaDev.Base.Data.Services;
using SavaDev.Base.Data.Services.Interfaces;

namespace SavaDev.Service.Drafts
{
    public interface IDraftService : IEntityRegistryService<DraftModel, DraftFilterModel>
    {
        Task<OperationResult> Create<T>(DraftModel model, T contentModel);

        Task<OperationResult> Update<T>(Guid id, T contentModel);

        Task<OperationResult> SetContentId(Guid id, string contentId);

        Task<OperationResult> Delete(Guid id);

        Task<DraftModel> GetOne(Guid id);

        Task<ItemsPage<DraftModel>> GetAll(RegistryQuery query);
    }
}
