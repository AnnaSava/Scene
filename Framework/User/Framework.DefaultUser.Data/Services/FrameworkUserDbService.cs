﻿using AutoMapper;
using Framework.Base.DataService.Contract.Models;
using Framework.Base.DataService.Contract.Models.ListView;
using Framework.Base.DataService.Services;
using Framework.User.DataService.Contract.Interfaces;
using Framework.User.DataService.Contract.Models;
using Framework.User.DataService.Entities;
using Framework.User.DataService.Services.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.DataService.Services
{
    public class FrameworkUserDbService : BaseUserDbService<FrameworkUser>, IFrameworkUserDbService
    {
        private readonly Dictionary<string, string> FieldNamesDiff = new Dictionary<string, string>
        {
            { "login", "username" }
        };

        public FrameworkUserDbService(
            FrameworkUserDbContext dbContext, 
            IUserManagerAdapter<FrameworkUser> userManagerAdapter, 
            ISignInManagerAdapter signInManagerAdapter,      
            IMapper mapper)
            : base(dbContext, userManagerAdapter, signInManagerAdapter, mapper)
        {

        }

        public async Task<TUserModelOut> Create<TUserModelOut>(FrameworkUserFormModel model, string password)
        {
            return await _userManagerAdapter.CreateUser<FrameworkUser, FrameworkUserFormModel, TUserModelOut>(model, password, _mapper);
        }

        public async Task<TUserModelOut> Update<TUserModelOut>(FrameworkUserFormModel model)
        {
            return await _userManagerAdapter.UpdateUser<FrameworkUser, FrameworkUserFormModel, TUserModelOut>(model, _dbContext, _mapper);
        }

        public async Task<FrameworkUserModel> GetOne(long id)
        {
            // TODO разобраться с include
            return await GetOne<FrameworkUserModel>(id, null);
        }

        public async Task<T> GetOne<T>(long id, string include) where T : IUserModel
        {
            // TODO разобраться с include
            static void Include(ref IQueryable<FrameworkUser> list, string include)
            {
                //_dbContext.Users.Where(m => m.Id == 1).Include(m => m.UserClaims);
                if (include == "userClaims")
                {
                    // list.Include(m => m.UserClaims);
                }
            }

            return await _dbContext.GetOne<FrameworkUser, T>(id, _mapper, Include, include);
        }

        public async Task<PageListModel<FrameworkUserModel>> GetAll(ListQueryModel<FrameworkUserFilterModel> query)
        {
            static void ApplyFilters(ref IQueryable<FrameworkUser> list, FrameworkUserFilterModel filter)
            {
                list = list.ApplyFilters(filter);
            }

            query.PageInfo.Sort = GetChangedSortFields(query.PageInfo.Sort, FieldNamesDiff);

            return await _dbContext.GetAll<FrameworkUser, FrameworkUserModel, FrameworkUserFilterModel>(query, ApplyFilters, _mapper);
        }
    }
}
