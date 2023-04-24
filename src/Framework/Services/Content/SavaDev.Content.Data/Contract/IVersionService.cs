using SavaDev.Content.Data.Contract.Models;
using SavaDev.Base.Data.Registry;
using SavaDev.Base.Data.Services;
using SavaDev.Base.Data.Services.Interfaces;
using SavaDev.Base.Data.Models.Interfaces;

namespace SavaDev.Content.Data.Contract
{
    public interface IVersionService : IEntityRegistryService<VersionModel, VersionFilterModel>
    {
        Task<OperationResult> Create<T>(VersionModel model, T contentModel) where T : IContentJsonSerializable;
    }
}
