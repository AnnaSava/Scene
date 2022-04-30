using Framework.Base.Service.ListView;
using Framework.Base.Types.Validation;
using Framework.User.Service.Contract.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.Service.Contract.Interfaces
{
    public interface IFrameworkUserService
    {
        Task<ListPageViewModel<FrameworkUserViewModel>> GetAll(FrameworkUserFilterViewModel filter, ListPageInfoViewModel pageInfo);

        Task<FrameworkUserViewModel> Create(FrameworkUserFormViewModel model, string password);

        Task<FrameworkUserViewModel> Update(FrameworkUserFormViewModel model);

        Task<FrameworkUserViewModel> Delete(long id);

        Task<FrameworkUserViewModel> Restore(long id);

        Task<FrameworkUserViewModel> Lock(UserLockoutViewModel lockoutModel);

        Task<FrameworkUserViewModel> Unlock(long id);

        Task<SignInResult> SignIn(string identifier, string password, bool rememberMe);

        Task SignOut();

        Task<IFrameworkUserViewModel> GetOne(long id, string target);

        Task<FrameworkUserViewModel> GetOneByEmail(string email);

        Task<Dictionary<string, bool>> CheckExists(string email, string login);

        Task<FrameworkUserViewModel> Register(FrameworkRegisterViewModel model);

        Task<FieldValidationResult> ValidateField(string name, string value);

        Task AddRoles(long id, UserRolesFormViewModel form);

        Task RemoveRoles(long id, UserRolesFormViewModel form);
    }
}
