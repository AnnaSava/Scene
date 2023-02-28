using Framework.Base.Service.ListView;
using SavaDev.Base.Data.Models;
using SavaDev.Users.Front.Contract.Models;

namespace SavaDev.Users.Front.Contract
{
    public interface IUserViewService
    {
        Task<ListPageViewModel<UserViewModel>> GetAll(UserFilterViewModel filter, ListPageInfoViewModel pageInfo);

        Task<UserViewModel> Create(UserFormViewModel model, string password);

        Task<UserViewModel> Update(UserFormViewModel model);

        Task<UserViewModel> Delete(long id);

        Task<UserViewModel> Restore(long id);

       //todo Task<AppUserViewModel> Lock(UserLockoutViewModel lockoutModel);

        Task<UserViewModel> Unlock(long id);

        Task<IUserViewModel> GetOne(long id, string target);

        Task<UserViewModel> GetOneByEmail(string email);

        Task<Dictionary<string, bool>> CheckExists(string email, string login);

        Task<FieldValidationResult> ValidateField(string name, string value);

        //Task AddRoles(long id, UserRolesFormViewModel form);

        //Task RemoveRoles(long id, UserRolesFormViewModel form);
    }
}
