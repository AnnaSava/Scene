using AutoMapper;
using Framework.User.DataService.Contract.Interfaces;
using Framework.User.DataService.Contract.Models;
using Framework.User.DataService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.DataService.Services
{
    public class AppRoleDbService : BaseRoleDbService<AppRole, RoleClaim, AppRoleModel, AppRoleFilterModel>, IAppRoleDbService
    {
        public AppRoleDbService(
           AppUserContext dbContext,
           IRoleManagerAdapter<AppRole> roleManagerAdapter,
           IMapper mapper)
           : base(dbContext, roleManagerAdapter, mapper, nameof(AppRoleDbService))
        {

        }

        protected override void ApplyFilters(ref IQueryable<AppRole> list, AppRoleFilterModel filter)
        {
            list = list.ApplyFilters(filter);
        }
    }
}
