using Framework.Base.DataService.Contract.Interfaces;
using Framework.User.DataService.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.DataService.Contract.Interfaces
{
    public interface IRoleContext<TRole, TRoleClaim> : IDbContext
        where TRole : BaseRole
        where TRoleClaim: IdentityRoleClaim<long>
    {
        DbSet<TRole> Roles { get; set; }

        DbSet<TRoleClaim> RoleClaims { get; set; }
    }
}
