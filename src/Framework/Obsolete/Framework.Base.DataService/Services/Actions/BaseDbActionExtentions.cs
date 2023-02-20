using Framework.Base.DataService.Contract.Interfaces;
using Framework.Base.DataService.Contract.Models;
using Framework.Base.DataService.Entities;
using Framework.Base.DataService.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.Base.DataService.Services
{
    [Obsolete]
    public delegate void IncludeDelegate<TEntity>(ref IQueryable<TEntity> list, string include) where TEntity : class, IEntity<long>;
    [Obsolete]
    public delegate void ApplyFiltersDelegate<TEntity, TFilterModel>(ref IQueryable<TEntity> list, TFilterModel filter) 
        where TEntity : class, IEntity<long>
        where TFilterModel : ListFilterModel, new();
    [Obsolete]
    public delegate void ApplyIntKeyFiltersDelegate<TEntity, TFilterModel>(ref IQueryable<TEntity> list, TFilterModel filter)
        where TEntity : class, IEntity<int>
        where TFilterModel : IFilter, new();
    [Obsolete]
    public delegate void ApplySimpleFiltersDelegate<TEntity, TFilterModel>(ref IQueryable<TEntity> list, TFilterModel filter)
        where TEntity : class, IAnyEntity
        where TFilterModel : IFilter, new();
    [Obsolete]
    public delegate void AddingDelegate<TEntity>(TEntity entity) where TEntity : class, IAnyEntity;
    [Obsolete]
    public delegate Task DeletingDelegate<TEntity>(TEntity entity) where TEntity : class, IAnyEntity;
    [Obsolete]
    public delegate void SavingDelegate<TEntity>(TEntity entity) where TEntity : class, IAnyEntity;
    [Obsolete]
    public delegate Task SavingAsyncDelegate<TEntity>(TEntity entity) where TEntity : class, IAnyEntity;

    [Obsolete]
    public static class BaseDbActionExtentions
    {
        [Obsolete]
        public static async Task<TEntity> GetEntityForRestore<TEntity>(this IDbContext dbContext, long id)
            where TEntity : class, IEntity<long>, IEntityRestorable
        {
            var entity = await dbContext.Set<TEntity>().FirstOrDefaultAsync(m => m.Id == id);

            if (entity == null)
                throw new EntityNotFoundException(
                          typeof(BaseDbActionExtentions),
                          nameof(GetEntityForRestore),
                          typeof(TEntity).Name,
                          id);

            entity.LastUpdated = DateTime.UtcNow;

            return entity;
        }
        [Obsolete]
        public static async Task<TEntity> GetEntityForUpdate<TEntity>(this IDbContext dbContext, int id)
            where TEntity : class, IEntity<int>, IEntityRestorable
        {
            var entity = await dbContext.Set<TEntity>().FirstOrDefaultAsync(m => m.Id == id && m.IsDeleted == false);

            if (entity == null)
                throw new EntityNotFoundException(
                          typeof(BaseDbActionExtentions),
                          nameof(GetEntityForUpdate),
                          typeof(TEntity).Name,
                          id);

            entity.LastUpdated = DateTime.UtcNow;

            return entity;
        }
        [Obsolete]
        public static async Task<TEntity> GetEntityForRestore<TEntity>(this IDbContext dbContext, int id)
            where TEntity : class, IEntity<int>, IEntityRestorable
        {
            var entity = await dbContext.Set<TEntity>().FirstOrDefaultAsync(m => m.Id == id);

            if (entity == null)
                throw new EntityNotFoundException(
                          typeof(BaseDbActionExtentions),
                          nameof(GetEntityForRestore),
                          typeof(TEntity).Name,
                          id);

            entity.LastUpdated = DateTime.UtcNow;

            return entity;
        }
    }
}
