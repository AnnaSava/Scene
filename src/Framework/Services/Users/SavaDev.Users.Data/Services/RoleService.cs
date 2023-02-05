using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SavaDev.Base.Data.Context;
using SavaDev.Base.User.Data.Services;

namespace SavaDev.Users.Data
{
    public class RoleService : BaseRoleDbService<Role, RoleModel>, IRoleService
    {
        public RoleService(
            IDbContext dbContext,
            RoleManager<Role> roleManager,
            IMapper mapper,
            ILogger<RoleService> logger)
            : base(dbContext, roleManager, mapper, logger)
        {

        }
    }
}
