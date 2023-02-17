using SavaDev.Base.Data.Registry;
using SavaDev.Base.Data.Services;
using SavaDev.Base.User.Data.Models.Interfaces;
using SavaDev.Users.Data.Contract.Models;

namespace SavaDev.Users.Data
{
    public interface IRoleService 
    {
        Task<OperationResult> Create(RoleModel model);

        Task<OperationResult> Update(long id, RoleModel model);

        Task<OperationResult> Delete(long id);

        Task<OperationResult> Restore(long id);

        Task<TModel> GetOne<TModel>(long id) where TModel : BaseRoleModel;

        Task<IEnumerable<TModel>> GetByNames<TModel>(IEnumerable<string> names);

        Task<bool> CheckNameExists(string name);

        //Task<RegistryPage<TItemModel>> GetRegistryPage<TFilterModel, TItemModel>(RegistryQuery<TFilterModel> query);
    }
}
