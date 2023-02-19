using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SavaDev.Base.Data.Context;
using SavaDev.Base.Users.Security.Account;
using SavaDev.Base.Users.Security.Contract;
using SavaDev.Users.Data.Entities;

namespace SavaDev.Users.Front.Security
{
    public class WebAccountService : AccountService<long, User>, IAccountService
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

