using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Sava.Users.Front.Contract;
using SavaDev.Base.Data.Context;
using SavaDev.Base.Users.Security.Account;
using SavaDev.Users.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Users.Front.Security
{
    public class WebAccountService : AccountService<long, User>, IWebAccountService
    {
        public WebAccountService(
            IDbContext dbContext,
            UserManager<Data.Entities.User> userManager,
            SignInManager<Data.Entities.User> signInManager,
            IMapper mapper,
            ILogger<WebAccountService> logger)
            : base(dbContext, userManager, signInManager, mapper, logger)
        {

        }
    }
}

