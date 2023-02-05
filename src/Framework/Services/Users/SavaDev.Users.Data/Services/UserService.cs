using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SavaDev.Base.Data.Context;
using SavaDev.Base.User.Data.Services;
using SavaDev.Users.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Users.Data
{
    public class UserService : BaseUserDbService<User, UserFormModel>, IUserService
    {
        public UserService(IDbContext dbContext, UserManager<User> userManager, IMapper mapper, ILogger<UserService> logger)
            : base(dbContext, userManager, mapper, logger)
        {

        }
    }
}
