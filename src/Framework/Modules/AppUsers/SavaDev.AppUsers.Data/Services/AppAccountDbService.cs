using AutoMapper;
using Framework.DefaultUser.Data.Contract;
using Framework.User.DataService.Contract.Interfaces;
using Framework.User.DataService.Entities;
using Framework.User.DataService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.DefaultUser.Data.Services
{
    public class AppAccountDbService : BaseAccountDbService<AppUser>, IAppAccountDbService
    {
        public AppAccountDbService(
            IUserContext<AppUser> dbContext,
            IUserManagerAdapter<AppUser> userManagerAdapter,
            ISignInManagerAdapter signInManagerAdapter,
            IMapper mapper) : base(dbContext, userManagerAdapter, signInManagerAdapter, mapper)
        {
        }
    }
}
