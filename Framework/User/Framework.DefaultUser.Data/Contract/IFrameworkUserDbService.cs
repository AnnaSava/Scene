using Framework.User.DataService.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.DataService.Contract.Interfaces
{
    public interface IFrameworkUserDbService : IUserSearchDbService<FrameworkUserModel, FrameworkUserFilterModel>
    {
        // Task<UserModel> Create(UserFormModel model, string password);

        Task<TUserModelOut> Create<TUserModelOut>(FrameworkUserFormModel model, string password);

        Task<TUserModelOut> Update<TUserModelOut>(FrameworkUserFormModel model);

        Task<TUserModelOut> Delete<TUserModelOut>(long id);

        Task<TUserModelOut> Restore<TUserModelOut>(long id);

        Task<FrameworkUserModel> Lock(UserLockoutModel lockoutModel);

        Task<FrameworkUserModel> Unlock(long id);

        Task<FrameworkUserModel> GetOne(long id);

        Task<T> GetOne<T>(long id, string include) where T : IUserModel;

        Task<FrameworkUserModel> GetOneByLogin(string login);

        Task<FrameworkUserModel> GetOneByEmail(string email);

        Task<bool> CheckEmailExists(string email);

        Task<bool> CheckUserNameExists(string userName);

        Task AddRoles(UserRolesModel model);

        Task RemoveRoles(UserRolesModel model);

        Task<string> GenerateEmailConfirmationToken(string email);

        Task<bool> ConfirmEmail(string email, string token);

        Task<TUserModelOut> GetOneByLoginOrEmail<TUserModelOut>(string loginOrEmail);

        Task<string> GeneratePasswordResetToken(string email);

        Task ResetPassword(string email, string token, string newPassword);

        Task<SignInResultModel<TUserOutModel>> SignIn<TUserOutModel>(LoginModel model) where TUserOutModel : BaseUserModel;
    }
}
