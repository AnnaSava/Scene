using Framework.User.DataService.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.DataService.Contract.Interfaces
{
    public interface IAppUserDbService : IUserSearchDbService<AppUserModel, AppUserFilterModel>
    {
        // Task<UserModel> Create(UserFormModel model, string password);

        Task<TUserModelOut> Create<TUserModelOut>(AppUserFormModel model, string password);

        Task<TUserModelOut> Update<TUserModelOut>(AppUserFormModel model);

        Task<TUserModelOut> Delete<TUserModelOut>(long id);

        Task<TUserModelOut> Restore<TUserModelOut>(long id);

        Task<FrameworkUserModel> Lock<FrameworkUserModel>(UserLockoutModel lockoutModel);

        Task<FrameworkUserModel> Unlock<FrameworkUserModel>(long id);

        Task<AppUserModel> GetOne(long id);

        Task<T> GetOne<T>(long id, string include) where T : IUserModel;

        Task<FrameworkUserModel> GetOneByLogin<FrameworkUserModel>(string login);

        Task<FrameworkUserModel> GetOneByEmail<FrameworkUserModel>(string email);

        Task<bool> CheckEmailExists(string email);

        Task<bool> CheckUserNameExists(string userName);

        Task AddRoles(UserRolesModel model);

        Task RemoveRoles(UserRolesModel model);

        Task<TUserModelOut> GetOneByLoginOrEmail<TUserModelOut>(string loginOrEmail);
    }
}
