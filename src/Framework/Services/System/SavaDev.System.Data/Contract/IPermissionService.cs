using SavaDev.Base.Data.Services;
using SavaDev.Base.Data.Services.Interfaces;
using SavaDev.System.Data.Contract.Models;

namespace SavaDev.System.Data.Contract
{
    public interface IPermissionService : IEntityRegistryService<PermissionModel, PermissionFilterModel>
    {
        Task<OperationResult> Create(PermissionModel model);

        Task<IEnumerable<string>> FilterExisting(IEnumerable<string> names);

        Task<Dictionary<string, List<string>>> GetTree();
    }
}
