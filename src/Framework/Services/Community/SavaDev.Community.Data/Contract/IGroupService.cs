using SavaDev.Base.Data.Registry;
using SavaDev.Base.Data.Services;
using SavaDev.Community.Data;
using SavaDev.Community.Data.Contract.Models;

namespace SavaDev.Community.Service.Contract
{
    public interface IGroupService
    {
        Task<OperationResult> Create(GroupModel model);

        Task<GroupModel> GetOne<GroupModel>(Guid id);

        Task<RegistryPage<RoleModel>> GetRegistryPage(RegistryQuery<GroupFilterModel> query);
    }
}
