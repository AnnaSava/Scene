using SavaDev.Base.Data.Registry;
using SavaDev.Base.Data.Services;
using SavaDev.Base.Data.Services.Interfaces;
using SavaDev.Community.Data;
using SavaDev.Community.Data.Contract.Models;

namespace SavaDev.Community.Service.Contract
{
    public interface IGroupService : IEntityRegistryService<GroupModel, GroupFilterModel>
    {
        Task<OperationResult> Create(GroupModel model);

        Task<GroupModel> GetOne<GroupModel>(Guid id);
        
    }
}
