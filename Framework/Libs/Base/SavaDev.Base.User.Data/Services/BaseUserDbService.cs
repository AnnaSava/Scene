﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SavaDev.Base.Data.Models.Interfaces;
using SavaDev.Base.User.Data.Adapters.Interfaces;
using SavaDev.Base.User.Data.Context;
using SavaDev.Base.User.Data.Entities;
using SavaDev.Base.User.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SavaDev.Base.User.Data.Services
{
    public class BaseUserDbService<TUserEntity>
        where TUserEntity : BaseUser
    {
        protected readonly IUserContext<TUserEntity> _dbContext;
        protected IUserManagerAdapter<TUserEntity> _userManagerAdapter;
        private readonly ISignInManagerAdapter _signInManagerAdapter;
        protected readonly IMapper _mapper;

        public BaseUserDbService(
            IUserContext<TUserEntity> dbContext, 
            IUserManagerAdapter<TUserEntity> userManagerAdapter,
            ISignInManagerAdapter signInManagerAdapter,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _userManagerAdapter = userManagerAdapter;
            _signInManagerAdapter = signInManagerAdapter;
            _mapper = mapper;
        }

        public async Task<TUserModel> Create<TUserModel>(TUserModel model, string password)
        {
            return await _userManagerAdapter.CreateUser<TUserEntity, TUserModel, TUserModel>(model, password, _mapper);
        }

        public async Task<TUserModelOut> Create<TUserModelIn, TUserModelOut>(TUserModelIn model, string password)
        {
            return await _userManagerAdapter.CreateUser<TUserEntity, TUserModelIn, TUserModelOut>(model, password, _mapper);
        }

        public async Task<TUserModelOut> GetOneByLoginOrEmail<TUserModelOut>(string loginOrEmail)
        {
            var user = await _userManagerAdapter.GetOneByLoginOrEmail(loginOrEmail);
            return _mapper.Map<TUserModelOut>(user);
        }

        public async Task<TUserModelOut> GetOneByLogin<TUserModelOut>(string login)
        {
            return await _dbContext.GetUserByLogin<TUserEntity, TUserModelOut>(login, _mapper);
        }

        public async Task<TUserModelOut> GetOneByEmail<TUserModelOut>(string email)
        {
            return await _dbContext.GetUserByEmail<TUserEntity, TUserModelOut>(email, _mapper);
        }

        public async Task<TUserModelOut> Lock<TUserModelOut>(UserLockoutModel lockoutModel)
        {
            var entity = await _dbContext.Users.FirstOrDefaultAsync(m => m.Id == lockoutModel.Id);
            entity.LockoutEnabled = true;
            entity.LockoutEnd = lockoutModel.LockoutEnd;

            var lockout = new Lockout()
            {
                LockDate = DateTime.Now,
                LockedByUserId = 1, //TODO прокидывать юзера, кто заблокировал
                LockoutEnd = lockoutModel.LockoutEnd,
                Reason = lockoutModel.Reason,
                UserId = lockoutModel.Id
            };
            _dbContext.Lockouts.Add(lockout);

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<TUserModelOut>(entity);
        }

        public async Task<TUserModelOut> Unlock<TUserModelOut>(long id)
        {
            var entity = await _dbContext.Users.FirstOrDefaultAsync(m => m.Id == id);
            entity.LockoutEnabled = false;
            entity.LockoutEnd = null;
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<TUserModelOut>(entity);
        }

        public async Task<bool> CheckEmailExists(string email)
        {
            return await _dbContext.CheckUserEmailExists<TUserEntity>(email);
        }

        public async Task<bool> CheckUserNameExists(string userName)
        {
            return await _dbContext.CheckUserLoginExists<TUserEntity>(userName);
        }

        public async Task UpdateRoles(UserRolesModel model)
        {
            var userRoles = await _userManagerAdapter.GetRolesAsync(model.UserId);

            var rolesToAdd = model.RoleNames.Except(userRoles);

            if (rolesToAdd.Any())
            {
                await _userManagerAdapter.AddToRolesAsync(model.UserId, rolesToAdd);
            }

            var rolesToRemove = userRoles.Except(model.RoleNames);
            if (rolesToRemove.Any())
            {
                await _userManagerAdapter.RemoveFromRolesAsync(model.UserId, rolesToRemove);
            }
        }

        public async Task AddRoles(UserRolesModel model)
        {
            await _userManagerAdapter.AddToRolesAsync(model.UserId, model.RoleNames);
        }

        public async Task RemoveRoles(UserRolesModel model)
        {
            await _userManagerAdapter.RemoveFromRolesAsync(model.UserId, model.RoleNames);
        }

        public async Task<IList<string>> GetRoleNames(long id)
        {
            return await _userManagerAdapter.GetRolesAsync(id);
        }


        // TODO подумать, куда перенести, т.к. в теории может пригодиться не только для пользователей
        //protected IEnumerable<ListSortModel> GetChangedSortFields(IEnumerable<ListSortModel> sortList, Dictionary<string, string> diff)
        //{
        //    if (sortList == null) return null;

        //    var newSortList = new List<ListSortModel>();

        //    foreach (var sort in sortList)
        //    {
        //        var newSort = sort;
        //        var fieldName = sort.FieldName.ToLower();
        //        if (diff.ContainsKey(fieldName))
        //        {
        //            newSort.FieldName = diff[fieldName];
        //        }

        //        newSortList.Add(newSort);
        //    }
        //    return newSortList;
        //}
    }
}
