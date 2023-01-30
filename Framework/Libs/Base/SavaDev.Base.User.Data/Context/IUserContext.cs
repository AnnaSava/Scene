using Microsoft.EntityFrameworkCore;
using SavaDev.Base.Data.Context;
using SavaDev.Base.User.Data.Entities;

namespace SavaDev.Base.User.Data.Context
{
    public interface IUserContext<TEntity> : IDbContext
        where TEntity : BaseUser
    {
        DbSet<TEntity> Users { get; set; }

        DbSet<Lockout> Lockouts { get; set; }
    }
}
