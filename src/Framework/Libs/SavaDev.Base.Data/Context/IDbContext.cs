using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SavaDev.Base.Data.Entities.Interfaces;
using System.Linq.Expressions;

namespace SavaDev.Base.Data.Context
{
    public interface IDbContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        ValueTask<EntityEntry<TEntity>> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        DatabaseFacade Database { get; }

        Task<TEntity> GetEntityForChange<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class;

        Task<TEntity> GetEntityForChange<TKey, TEntity>(TKey id, bool restore = false) where TEntity : class, IEntity<TKey>;
    }
}
