using AutoMapper;
using AutoMapper.QueryableExtensions;
using Framework.Base.DataService.Contract.Interfaces;
using Framework.Base.DataService.Contract.Models;
using Framework.Base.DataService.Contract.Models.ListView;
using Framework.Base.DataService.Entities;
using Framework.Base.DataService.Exceptions;
using Framework.Base.Exceptions;
using Framework.Base.Types.ModelTypes;
using Framework.Helpers.TypeHelpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

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
        public static async Task<TModel> Create<TEntity, TModel>(this IDbContext dbContext, TModel model, IMapper mapper, AddingDelegate<TEntity> OnAdding)
            where TEntity : class, IAnyEntity
            where TModel : IAnyModel
        {
            var newEntity = mapper.Map<TEntity>(model);
            OnAdding?.Invoke(newEntity);
            var addResult = await dbContext.AddAsync(newEntity);
            await dbContext.SaveChangesAsync();

            return mapper.Map<TModel>(addResult.Entity);
        }
        [Obsolete]
        public static async Task<PageListModel<TModel>> GetAll<TEntity, TModel, TFilterModel>(this IDbContext dbContext,
            ListQueryModel<TFilterModel> query,
            ApplyFiltersDelegate<TEntity, TFilterModel> applyFilters,
            IMapper mapper) 
            where TEntity : class, IEntity<long>
            where TFilterModel : ListFilterModel, new()
        {
            var list = dbContext.Set<TEntity>().AsQueryable();

            applyFilters?.Invoke(ref list, query.Filter);

            if (query.PageInfo.Sort?.Any() ?? false)
            {
                //list = list.OrderBy(query.PageInfo.Sort.Select(s => new OrderByInfo() { Direction = s.Direction, PropertyName = s.FieldName, Initial = s.Initial }));
            }
            else
            {
                list = list.OrderBy(m => m.Id);
            }

            var res = await list.ProjectTo<TModel>(mapper.ConfigurationProvider).ToPagedListAsync(query.PageInfo.PageNumber, query.PageInfo.RowsCount);

            var page = new PageListModel<TModel>()
            {
                Items = res,
                Page = res.PageNumber,
                TotalPages = res.PageCount,
                TotalRows = res.TotalItemCount
            };

            return page;
        }
        [Obsolete]
        public static async Task<TEntity> GetEntityForView<TEntity>(this IDbContext dbContext, long id)
            where TEntity : class, IEntity<long>, IEntityRestorable
        {
            var entity = await dbContext.Set<TEntity>().FirstOrDefaultAsync(m => m.Id == id && m.IsDeleted == false);
            return entity;
        }
        [Obsolete]
        public static async Task<TEntity> GetEntityForUpdate<TEntity>(this IDbContext dbContext, long id)
            where TEntity : class, IEntity<long>, IEntityRestorable
        {
            var entity = await dbContext.Set<TEntity>().FirstOrDefaultAsync(m => m.Id == id && m.IsDeleted == false);

            if (entity == null)
                throw new EntityNotFoundException(); // TODO значение! почему-то падает тест в таком виде:
            //throw new EntityNotFoundException(
            //              typeof(BaseDbActionExtentions),
            //              nameof(GetEntityForRestore),
            //              typeof(TEntity).Name,
            //              id);

            entity.LastUpdated = DateTime.UtcNow;

            return entity;
        }
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
