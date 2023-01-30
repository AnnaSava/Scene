using Framework.Base.DataService.Contract.Interfaces;
using Framework.User.DataService.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Framework.User.DataService.Contract.Interfaces
{
    public interface IAuthTokenContext : IDbContext
    {
        DbSet<AuthToken> AuthTokens { get; set; }
    }
}
