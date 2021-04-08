using AutoMapper;
using AutoMapper.QueryableExtensions;
using Framework.Base.DataService.Contract.Interfaces;
using Framework.Base.DataService.Contract.Models;
using Framework.Base.DataService.Entities;
using Framework.Base.DataService.Exceptions;
using Framework.Base.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Threading.Tasks;
using X.PagedList;

namespace Framework.Base.DataService.Services
{
    public abstract class BaseDbService<TEntity, TModel> : IBaseDbService<TModel>
        where TEntity : BaseEntity<long>
        where TModel : BaseModel<long>
    {
        protected readonly DbContext _dbContext;
        protected readonly IMapper _mapper;

        public BaseDbService(DbContext dbContext, IMapper mapper, string serviceName)
        {
            _dbContext = dbContext ?? throw new ProjectArgumentException(
                GetType(),
                serviceName,
                nameof(dbContext),
                null);

            _mapper = mapper ?? throw new ProjectArgumentException(
                GetType(),
                serviceName,
                nameof(mapper),
                null);
        }

        public async Task<TModel> Create(TModel model)
        {
            ValidateCreate(model);
            PrepareModelForCreate(model);
            TEntity newEntity =
                _mapper.Map<TEntity>(model);
            EntityEntry<TEntity> addResult = await _dbContext.AddAsync(newEntity);
            OnCreate(newEntity);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<TModel>(addResult.Entity);
        }

        public async Task<TModel> Update(TModel model)
        {
            ValidateUpdate(model);

            var currentEntity = await GetEntityById(model.Id);

            _mapper.Map(model, currentEntity);
            OnUpdate(currentEntity);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<TModel>(currentEntity);
        }

        public async Task<TModel> Delete(long id)
        {
            var entity = await GetEntityById(id);
            entity.IsDeleted = true;
            OnDelete(entity);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<TModel>(entity);
        }

    public async Task<TModel> Restore(long id)
        {
            var entity = await GetEntityById(id);
            entity.IsDeleted = false;
            OnRestore(entity);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<TModel>(entity);
        }

        public async Task<TModel> GetOne(long id)
        {
            var entity = await GetEntityById(id);

            return _mapper.Map<TModel>(entity);
        }

        // TODO: обернуть IPagedList в свой тип?
        public async Task<IPagedList<TModel>> GetAll(int pageNumber, int pageSize)
        {
            return await _dbContext
                .Set<TEntity>()
                .ProjectTo<TModel>(_mapper.ConfigurationProvider)
                .ToPagedListAsync(pageNumber, pageSize);
            //.ToListAsync();
        }

        /// <summary>
        /// Поиск сущности коллекции по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор коллекции</param>
        /// <returns>Возвращает Task of <see cref="TEntity"/></returns>
        protected async Task<TEntity> GetEntityById(long id)
        {
            if (id <= 0)
                throw new ProjectArgumentException(
                    GetType(),
                    nameof(GetEntityById),
                    nameof(id),
                    id);

            var entity = await _dbContext.Set<TEntity>()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (entity == null || entity.IsDeleted)
                throw new EntityNotFoundException(
                          GetType(),
                          nameof(GetEntityById),
                          typeof(TEntity).FullName,
                          id);
            return entity;
        }


        /// <summary>
        /// Внесение дополнительных данных в модель перед созданием коллекции
        /// </summary>
        /// <param name="model">DTO с типом уведомления</param>
        private static void PrepareModelForCreate(TModel model)
        {
            model.IsDeleted = false;
        }

        /// <summary>
        /// Валидация модели коллекции перед созданием коллекции
        /// </summary>
        /// <param name="model">Модель коллекции</param>
        protected virtual void ValidateCreate(TModel model)
        {
            if (model == null)
                throw new ProjectArgumentException(
                    GetType(),
                    nameof(ValidateCreate),
                    nameof(model),
                    null);
        }

        /// <summary>
        /// Валидация модели коллекции перед изменением коллекции
        /// </summary>
        /// <param name="model">DTO с типом уведомления</param>
        protected virtual void ValidateUpdate(TModel model)
        {
            if (model == null)
                throw new ProjectArgumentException(
                    GetType(),
                    nameof(ValidateUpdate),
                    nameof(model),
                    null);
        }

        protected virtual void OnCreate(TEntity entity) { }

        protected virtual void OnUpdate(TEntity entity) { }

        protected virtual void OnDelete(TEntity entity) { }

        protected virtual void OnRestore(TEntity entity) { }

        protected async Task<TModel> ChangeEntity(long id, Action<TEntity> action)
        {
            var entity = await GetEntityById(id);
            if (action != null)
            {
                action(entity);
                await _dbContext.SaveChangesAsync();
            }
            return _mapper.Map<TModel>(entity);
        }
    }
}
