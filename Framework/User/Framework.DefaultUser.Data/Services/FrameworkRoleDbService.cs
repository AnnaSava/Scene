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
    public class FrameworkRoleDbService : RoleDbService<FrameworkRole, RoleClaim, FrameworkRoleModel, FrameworkRoleFilterModel>, IFrameworkRoleDbService
    {
        public FrameworkRoleDbService(
           FrameworkUserDbContext dbContext,
           IRoleManagerAdapter<FrameworkRole> roleManagerAdapter,
           IMapper mapper)
           : base(dbContext, roleManagerAdapter, mapper, nameof(FrameworkRoleDbService))
        {

        }

        protected override void ApplyFilters(ref IQueryable<FrameworkRole> list, FrameworkRoleFilterModel filter)
        {
            list = list.ApplyFilters(filter);
        }
    }
}
