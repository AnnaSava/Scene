using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SavaDev.Base.Data.Context;
using SavaDev.Base.Data.Entities.Interfaces;
using SavaDev.Base.Data.Exceptions;
using SavaDev.Base.Data.Managers;
using SavaDev.Base.Data.Models.Interfaces;
using SavaDev.Base.Data.Registry;
using SavaDev.Base.Data.Registry.Filter;
using X.PagedList;

namespace SavaDev.Base.Data.Services
{
    public class BaseRestorableEntityService<TKey, TEntity, TFormModel> : BaseEntityService<TEntity, TFormModel>
         where TEntity : class, IEntity<TKey>, IEntityRestorable, new()
        where TFormModel : IFormModel, IAnyModel
    {
        #region Protected Properties: Managers

        protected CreateManager<TEntity, TFormModel> CreateManager { get; }
        protected UpdateManager<TKey, TEntity, TFormModel> UpdateManager { get; }
        protected UpdateRestorableSelector<TKey, TEntity> UpdateSelector { get; }
        protected UpdateFieldManager<TKey, TEntity> FieldSetterManager { get; }
        protected UpdateFieldManager<TKey, TEntity> RestorManager { get; }
        protected RestoreSelector<TKey, TEntity> RestoreSelector { get; }
        protected OneRestorableSelector<TKey, TEntity> OneSelector { get; }

        #endregion

        #region Public Constructors

        public BaseRestorableEntityService(IDbContext dbContext, IMapper mapper, ILogger logger) : base (dbContext, mapper, "", logger)
        {
            //creatorManager = new BaseCreatorManager<TEntity, TFormModel>(dbContext, mapper, logger);
            CreateManager = new CreateManager<TEntity, TFormModel>(GetInftrastructure);
            UpdateSelector = new UpdateRestorableSelector<TKey, TEntity>(dbContext, mapper, logger);
            UpdateManager = new UpdateManager<TKey, TEntity, TFormModel>(dbContext, mapper, logger, UpdateSelector);
            FieldSetterManager = new UpdateFieldManager<TKey, TEntity>(dbContext, mapper, logger, UpdateSelector);
            RestoreSelector = new RestoreSelector<TKey, TEntity>(dbContext, mapper, logger);
            RestorManager = new UpdateFieldManager<TKey, TEntity>(dbContext, mapper, logger, RestoreSelector);
            OneSelector = new OneRestorableSelector<TKey, TEntity>(dbContext, mapper, logger);
        }

        #endregion

        #region Public Methods: Mutation

        public async Task<OperationResult> Create(TFormModel model, Action<TEntity> onCreating = null, Action<TEntity> onCreated = null)
        {
            return await CreateManager.Create(model);
        }

        public async Task<OperationResult> Update(TKey id, TFormModel model, Action<TEntity> onUpdating = null, Action<TEntity> onUpdated = null)
        {
            return await UpdateManager.Update(id, model);
        }

        [Obsolete]
        public async Task<OperationResult> Update(TKey id, Action<TEntity> updatingMethod)
        {
            return await Update(id, null, updatingMethod);
        }

        [Obsolete]
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
                //_logger.LogError($"{nameof(Update)}: {ex.Message} {ex.StackTrace}");
                var result = new OperationResult(0, id, new OperationExceptionInfo(ex.Message));
                return result;
            }
        }

        public async Task<OperationResult> Delete(TKey id) => await FieldSetterManager.SetField(id, entity => entity.IsDeleted = true);

        public async Task<OperationResult> Restore(TKey id) => await RestorManager.SetField(id, entity => entity.IsDeleted = false);

        #endregion

        #region Public Methods: Query One

        // TODO возможно, include тут не нужен. Вроде как этапы и чекпойнты выбираются вместе с целью и так.
        public async Task<TModel> GetOne<TModel>(TKey id, Func<IQueryable<TEntity>, IQueryable<TEntity>> includeFunc = null)
            => await OneSelector.GetOne<TModel>(id, includeFunc);

        #endregion

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

            entity.LastUpdated = DateTime.UtcNow;

            return entity;
        }

        public async Task<IPagedList<TItemModel>> GetAllByRelated<TItemModel>(ByRelatedFilter<long> filter, RegistrySort sort, int page, int count)
        {
            var list = GetAllByRelatedQueryable(filter);

            list = list.ApplySort(sort);

            var res = await list
                .ProjectTo<TItemModel>(_mapper.ConfigurationProvider)
                .ToPagedListAsync(page, count);

            return res;
        }

        private IQueryable<TEntity> GetAllByRelatedQueryable(ByRelatedFilter<long> filter)
        {
            var list = _dbContext.Set<TEntity>()
                .AsQueryable()
                .AsNoTracking();

            list = list.ApplyByRelatedFilter(filter);

            if (!filter.WithDeleted)
            {
                list = list.Where(m => !m.IsDeleted);
            }
            return list;
        }
    }
}
