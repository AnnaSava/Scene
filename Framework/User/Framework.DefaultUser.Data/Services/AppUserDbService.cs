using AutoMapper;
using AutoMapper.QueryableExtensions;
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
    public class AppUserDbService : BaseUserDbService<AppUser>, IAppUserDbService
    {
        private readonly Dictionary<string, string> FieldNamesDiff = new Dictionary<string, string>
        {
            { "login", "username" }
        };

        public AppUserDbService(
            AppUserContext dbContext, 
            IUserManagerAdapter<AppUser> userManagerAdapter, 
            ISignInManagerAdapter signInManagerAdapter,      
            IMapper mapper)
            : base(dbContext, userManagerAdapter, signInManagerAdapter, mapper)
        {

        }

        public async Task<TUserModelOut> Create<TUserModelOut>(AppUserFormModel model, string password)
        {
            return await _userManagerAdapter.CreateUser<AppUser, AppUserFormModel, TUserModelOut>(model, password, _mapper);
        }

        public async Task<TUserModelOut> Update<TUserModelOut>(AppUserFormModel model)
        {
            return await _userManagerAdapter.UpdateUser<AppUser, AppUserFormModel, TUserModelOut>(model, _dbContext, _mapper);
        }

        public async Task<AppUserModel> GetOne(long id)
        {
            // TODO разобраться с include
            return await GetOne<AppUserModel>(id, null);
        }

        public async Task<T> GetOne<T>(long id, string include) where T : IUserModel
        {
            // TODO разобраться с include
            static void Include(ref IQueryable<AppUser> list, string include)
            {
                //_dbContext.Users.Where(m => m.Id == 1).Include(m => m.UserClaims);
                if (include == "userClaims")
                {
                    // list.Include(m => m.UserClaims);
                }
            }

            return await _dbContext.GetOne<AppUser, T>(id, _mapper, Include, include);
        }

        public async Task<PageListModel<AppUserModel>> GetAll(ListQueryModel<AppUserFilterModel> query)
        {
            static void ApplyFilters(ref IQueryable<AppUser> list, AppUserFilterModel filter)
            {
                list = list.ApplyFilters(filter);
            }

            query.PageInfo.Sort = GetChangedSortFields(query.PageInfo.Sort, FieldNamesDiff);

            return await _dbContext.GetAll<AppUser, AppUserModel, AppUserFilterModel>(query, ApplyFilters, _mapper);
        }

        public async Task<IEnumerable<AppUserModel>> GetAllByIds(IEnumerable<string> ids)
        {
            var longIds = ids.Select(m => long.Parse(m));

            var list = await _dbContext.Set<AppUser>()
                .Where(m => longIds.Contains(m.Id))
                .ProjectTo<AppUserModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return list;
        }
    }
}
