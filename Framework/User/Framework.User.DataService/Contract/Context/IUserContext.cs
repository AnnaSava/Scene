using Framework.Base.DataService.Contract.Interfaces;
using Framework.User.DataService.Entities;
using Microsoft.EntityFrameworkCore;

namespace Framework.User.DataService.Contract.Interfaces
{
    public interface IUserContext<TEntity> : IDbContext
        where TEntity : BaseUser
    {
        DbSet<TEntity> Users { get; set; }

        DbSet<Lockout> Lockouts { get; set; }
    }
}
