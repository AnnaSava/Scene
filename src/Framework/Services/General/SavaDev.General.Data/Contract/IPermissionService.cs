using SavaDev.Base.Data.Services;
using SavaDev.Base.Data.Services.Interfaces;
using SavaDev.General.Data.Contract.Models;

namespace SavaDev.General.Data.Contract
{
    public interface IPermissionService : IEntityRegistryService<PermissionModel, PermissionFilterModel>
    {
        Task<OperationResult> Create(PermissionModel model);

        Task<IEnumerable<string>> FilterExisting(IEnumerable<string> names);

        Task<Dictionary<string, List<string>>> GetTree();
    }
}
