using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SavaDev.Base.Data.Context;
using SavaDev.Base.User.Data.Services;
using SavaDev.Users.Data.Contract.Models;
using SavaDev.Users.Data.Entities;

namespace SavaDev.Users.Data
{
    public class RoleService : BaseRoleDbService<Role, RoleFormModel>, IRoleService
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
