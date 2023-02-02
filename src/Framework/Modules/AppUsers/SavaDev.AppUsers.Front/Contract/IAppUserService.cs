using Framework.Base.Service.ListView;
using Framework.Base.Types.Validation;
using Framework.User.Service.Contract.Models;

namespace Framework.User.Service.Contract.Interfaces
{
    public interface IAppUserService
    {
        Task<ListPageViewModel<AppUserViewModel>> GetAll(AppUserFilterViewModel filter, ListPageInfoViewModel pageInfo);

        Task<AppUserViewModel> Create(AppUserFormViewModel model, string password);

        Task<AppUserViewModel> Update(AppUserFormViewModel model);

        Task<AppUserViewModel> Delete(long id);

        Task<AppUserViewModel> Restore(long id);

       //todo Task<AppUserViewModel> Lock(UserLockoutViewModel lockoutModel);

        Task<AppUserViewModel> Unlock(long id);

        Task<IAppUserViewModel> GetOne(long id, string target);

        Task<AppUserViewModel> GetOneByEmail(string email);

        Task<Dictionary<string, bool>> CheckExists(string email, string login);

        Task<FieldValidationResult> ValidateField(string name, string value);

        //Task AddRoles(long id, UserRolesFormViewModel form);

        //Task RemoveRoles(long id, UserRolesFormViewModel form);
    }
}
