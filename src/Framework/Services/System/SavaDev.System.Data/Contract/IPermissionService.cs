using SavaDev.Base.Data.Services;
using SavaDev.System.Data.Contract.Models;

namespace SavaDev.System.Data.Contract
{
    public interface IPermissionService
    {
        Task<OperationResult<PermissionModel>> Create(PermissionModel model);

       // Task<PageListModel<PermissionModel>> GetAll(ListQueryModel<PermissionFilterModel> query);

        Task<IEnumerable<string>> FilterExisting(IEnumerable<string> names);

        Task<Dictionary<string, List<string>>> GetTree();
    }
}
