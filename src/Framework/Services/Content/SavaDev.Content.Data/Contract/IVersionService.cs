using SavaDev.Content.Data.Contract.Models;
using SavaDev.Base.Data.Registry;
using SavaDev.Base.Data.Services;

namespace SavaDev.Content.Data.Contract
{
    public interface IVersionService
    {
        Task<OperationResult> Create<T>(VersionModel model, T contentModel);

        Task<RegistryPage<DraftModel>> GetRegistryPage(RegistryQuery<DraftFilterModel> query);

        Task<ItemsPage<VersionModel>> GetAll(RegistryQuery<VersionFilterModel> query);
    }
}
