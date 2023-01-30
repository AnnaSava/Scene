using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SavaDev.Base.Data.Context;
using SavaDev.Base.Data.Entities.Interfaces;
using SavaDev.Base.Data.Exceptions;
using SavaDev.Base.Data.Registry;
using SavaDev.Base.Data.Registry.Filter;
using SavaDev.Base.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Data.Managers
{
    public class RestorableEntityManager<TKey, TEntity, TFormModel>
         where TEntity : class, IEntity<TKey>, IEntityRestorable
    {
        private readonly IMapper _mapper;
        private readonly IDbContext _dbContext;
        private readonly ILogger _logger;

        public RestorableEntityManager(IDbContext dbContext, IMapper mapper, ILogger logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<OperationResult<TFormModel>> Create(TFormModel model, Action<TEntity> onCreating = null, Action<TEntity> onCreated = null)
        {
            using var tran = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var newEntity = _mapper.Map<TEntity>(model);
                var addResult = await _dbContext.AddAsync(newEntity);

                onCreating?.Invoke(newEntity);
                var rows = await _dbContext.SaveChangesAsync();
                onCreated?.Invoke(addResult.Entity);

                var result = new OperationResult<TFormModel>(rows, _mapper.Map<TFormModel>(addResult.Entity));

                await tran.CommitAsync();

                return result;
            }
            catch (Exception ex)
            {
                await tran.RollbackAsync();
                _logger.LogError($"{nameof(Create)}: {ex.Message} {ex.StackTrace}");
                var result = new OperationResult<TFormModel>(0, model, new OperationExceptionInfo(ex.Message));
                return result;
            }
        }

        public async Task<OperationResult<TFormModel>> Update(TKey id, TFormModel model, Action<TEntity> onUpdating = null, Action<TEntity> onUpdated = null)
        {
            using var tran = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var currentEntity = await GetEntityForUpdate(id);
                _mapper.Map(model, currentEntity);

                onUpdating?.Invoke(currentEntity);
                var rows = await _dbContext.SaveChangesAsync();
                onUpdated?.Invoke(currentEntity);

                var result = new OperationResult<TFormModel>(rows, _mapper.Map<TFormModel>(currentEntity));

                await tran.CommitAsync();
                return result;
            }
            catch (Exception ex)
            {
                await tran.RollbackAsync();
                _logger.LogError($"{nameof(Update)}: {ex.Message} {ex.StackTrace}");
                var result = new OperationResult<TFormModel>(0, model, new OperationExceptionInfo(ex.Message));
                return result;
            }
        }

        public async Task<OperationResult> Update(TKey id, Action<TEntity> updatingMethod)
        {
            return await Update(id, null, updatingMethod);
        }

        public async Task<OperationResult> Update(TKey id, Func<IQueryable<TEntity>, IQueryable<TEntity>> include, Action<TEntity> updatingMethod)
        {
            using var tran = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var currentEntity = await GetEntityForUpdate(id, include);

                updatingMethod?.Invoke(currentEntity);
                var rows = await _dbContext.SaveChangesAsync();

                var result = new OperationResult<TFormModel>(rows, _mapper.Map<TFormModel>(currentEntity));

                await tran.CommitAsync();
                return result;
            }
            catch (Exception ex)
            {
                await tran.RollbackAsync();
                _logger.LogError($"{nameof(Update)}: {ex.Message} {ex.StackTrace}");
                var result = new OperationResult(0, id, new OperationExceptionInfo(ex.Message));
                return result;
            }
        }

        public async Task<OperationResult> Delete(TKey id, Action<TEntity> onDeleting = null, Action<TEntity> onDeleted = null)
        {
            using var tran = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var entity = await GetEntityForUpdate(id);
                entity.IsDeleted = true;

                onDeleting?.Invoke(entity);
                var rows = await _dbContext.SaveChangesAsync();
                onDeleted?.Invoke(entity);

                var result = new OperationResult(rows, id);

                await tran.CommitAsync();
                return result;
            }
            catch (Exception ex)
            {
                await tran.RollbackAsync();
                _logger.LogError($"{nameof(Delete)}: {ex.Message} {ex.StackTrace}");
                var result = new OperationResult(0, id, new OperationExceptionInfo(ex.Message));
                return result;
            }
        }

        public async Task<OperationResult> Restore(TKey id, Action<TEntity> onRestoring = null, Action<TEntity> onRestored = null)
        {
            using var tran = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var entity = await GetEntityForUpdate(id);
                entity.IsDeleted = false;

                onRestoring?.Invoke(entity);
                var rows = await _dbContext.SaveChangesAsync();
                onRestored?.Invoke(entity);

                var result = new OperationResult(rows, id);

                await tran.CommitAsync();
                return result;
            }
            catch (Exception ex)
            {
                await tran.RollbackAsync();
                _logger.LogError($"{nameof(Delete)}: {ex.Message} {ex.StackTrace}");
                var result = new OperationResult(0, id, new OperationExceptionInfo(ex.Message));
                return result;
            }
        }

        // TODO возможно, include тут не нужен. Вроде как этапы и чекпойнты выбираются вместе с целью и так.
        public async Task<TModel> GetOne<TModel>(TKey id, Func<IQueryable<TEntity>, IQueryable<TEntity>> includeFunc = null)
        {
            var elQuery = _dbContext.Set<TEntity>()
                .Where(m => m.Id.Equals(id) && m.IsDeleted == false);

            if (includeFunc != null)
            {
                elQuery = includeFunc(elQuery);
            }

            return await elQuery.ProjectTo<TModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<TEntity> GetEntityForUpdate(TKey id)
        {
            var entity = await _dbContext.Set<TEntity>().FirstOrDefaultAsync(m => m.Id.Equals(id) && m.IsDeleted == false);

            if (entity == null)
                throw new EntityNotFoundException();

            entity.LastUpdated = DateTime.Now;

            return entity;
        }

        public async Task<TEntity> GetEntityForUpdate(TKey id, Func<IQueryable<TEntity>, IQueryable<TEntity>> include)
        {
            var entityQuery = _dbContext.Set<TEntity>().AsQueryable();

            if (include != null)
            {
                entityQuery = include(entityQuery);
            }

            var entity = await entityQuery.FirstOrDefaultAsync(m => m.Id.Equals(id) && m.IsDeleted == false);

            if (entity == null)
                throw new EntityNotFoundException();

            entity.LastUpdated = DateTime.Now;

            return entity;
        }

        public async Task<TEntity> GetEntityForRestore(TKey id)
        {
            var entity = await _dbContext.Set<TEntity>().FirstOrDefaultAsync(m => m.Id.Equals(id));

            if (entity == null)
                throw new EntityNotFoundException();

            entity.LastUpdated = DateTime.Now;

            return entity;
        }

        public async Task<RegistryPage<TItemModel>> GetRegistryPage<TFilterModel, TItemModel>(RegistryQuery<TFilterModel> query)
            where TFilterModel : BaseFilter
        {
            var list = _dbContext.Set<TEntity>()
                .AsQueryable()
                .AsNoTracking()
                .ApplyFilters(query.Filter)
                .ApplySort(query.Sort);

            var res = await list
                .ProjectTo<TItemModel>(_mapper.ConfigurationProvider)
                .ToPage(query.PageInfo);

            var page = new RegistryPage<TItemModel>(res);
            return page;
        }

        public async Task<ItemsPage<TItemModel>> GetItemsPage<TFilterModel, TItemModel>(
        RegistryQuery<TFilterModel> query,
            Func<IQueryable<TEntity>, RegistryQuery<TFilterModel>, IQueryable<TEntity>> applyFilterExpression)
            where TFilterModel : BaseFilter
        {
            var list = _dbContext.Set<TEntity>()
                   .AsQueryable()
                   .AsNoTracking();

            applyFilterExpression(list, query);

            list = list.ApplyFilters(query.Filter)
                   .ApplySort(query.Sort);

            var res = await list
                .ProjectTo<TItemModel>(_mapper.ConfigurationProvider)
                .ToPage(query.PageInfo);

            var page = new ItemsPage<TItemModel>(res);
            return page;
        }
    }
}
