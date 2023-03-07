using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SavaDev.Base.Data.Context;
using SavaDev.Base.Users.Security;
using SavaDev.Users.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Users.Front.Security
{
    public class WebSecurityService : SecurityService<long, User, Role>, ISecurityService
    {
        public WebSecurityService(IUserProvider userProvider, IDbContext dbContext,
            UserManager<Data.Entities.User> userManager,
            RoleManager<Role> roleManager,
            IMapper mapper,
            ILogger<WebSecurityService> logger) : base(userProvider, dbContext, userManager, roleManager, mapper, logger)
        {
        }
    }
}
