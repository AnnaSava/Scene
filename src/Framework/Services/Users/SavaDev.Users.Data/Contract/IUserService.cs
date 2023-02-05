﻿using Framework.User.DataService.Contract.Interfaces;
using SavaDev.Base.Data.Services;
using SavaDev.Base.User.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Users.Data
{
    public interface IUserService // : IUserSearchDbService<AppUserModel, AppUserFilterModel>
    {
        Task<OperationResult<UserFormModel>> Create(UserFormModel model, string password);

        Task<OperationResult<UserFormModel>> Update(long id, UserFormModel model);

        Task<OperationResult> Delete(long id);

        Task<OperationResult> Restore(long id);

       // Task<OperationResult<UserModel>> Lock<UserModel>(UserLockoutModel lockoutModel);

        Task<OperationResult<UserModel>> Unlock<UserModel>(long id);

        //Task<UserModel> GetOne(long id);

        //Task<T> GetOne<T>(long id, string include) where T : IUserModel;
        Task<TModel> GetOneByLoginOrEmail<TModel>(string loginOrEmail) where TModel : BaseUserModel;

        Task<TModel> GetOneByLogin<TModel>(string login);

        Task<TModel> GetOneByEmail<TModel>(string email);

        Task<bool> CheckEmailExists(string email);

        Task<bool> CheckLoginExists(string userName);

        //Task<OperationResult> UpdateRoles(UserRolesModel model);

        //Task<OperationResult> AddRoles(UserRolesModel model);

        //Task<OperationResult> RemoveRoles(UserRolesModel model);

        //Task<IList<string>> GetRoleNames(long id);
    }
}
