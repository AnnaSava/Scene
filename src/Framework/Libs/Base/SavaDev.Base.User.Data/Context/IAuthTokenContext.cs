using Microsoft.EntityFrameworkCore;
using SavaDev.Base.Data.Context;
using SavaDev.Base.User.Data.Entities;

namespace SavaDev.Base.User.Data.Context
{
    public interface IAuthTokenContext : IDbContext
    {
        DbSet<AuthToken> AuthTokens { get; set; }
    }
}
