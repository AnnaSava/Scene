using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SavaDev.Base.Data.Context;
using SavaDev.Base.User.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.User.Data.Context
{
    public class BaseUserDbContext<TUser, TRole> : IdentityDbContext<TUser, TRole, long, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
        where TUser : BaseUser
        where TRole : BaseRole
    {
        private DbContextOptions _options;
        protected BaseDbOptionsExtension _dbOptionsExtension;

        public BaseUserDbContext() : base() { }

        public BaseUserDbContext(DbContextOptions options) : base(options)
        {
            _options = options;

            _dbOptionsExtension = options.Extensions.OfType<BaseDbOptionsExtension>().FirstOrDefault();

            if (_dbOptionsExtension == null)
                throw new Exception($"{nameof(BaseDbOptionsExtension)} not set in {nameof(BaseDbContext)}");
        }

    }
}
