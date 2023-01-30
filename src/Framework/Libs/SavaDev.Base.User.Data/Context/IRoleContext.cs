using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SavaDev.Base.Data.Context;
using SavaDev.Base.User.Data.Entities;

namespace SavaDev.Base.User.Data.Context
{
    public interface IRoleContext<TRole, TRoleClaim> : IDbContext
        where TRole : BaseRole
        where TRoleClaim: IdentityRoleClaim<long>
    {
        DbSet<TRole> Roles { get; set; }

        DbSet<TRoleClaim> RoleClaims { get; set; }
    }
}
