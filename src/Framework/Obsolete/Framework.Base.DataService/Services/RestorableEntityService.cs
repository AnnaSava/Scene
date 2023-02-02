using AutoMapper;
using Framework.Base.DataService.Contract.Interfaces;
using Framework.Base.DataService.Contract.Models.ListView;
using Framework.Base.DataService.Entities;
using Framework.Base.Types.ModelTypes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Framework.Base.DataService.Services
{
    public class RestorableEntityService<TEntity, TModel> : BaseEntityService<TEntity, TModel>, IRestorableEntityService<TModel>
          where TEntity : class, IEntityRestorable, IEntity<long>
          where TModel : IModelRestorable
    {
        public RestorableEntityService(IDbContext dbContext, IMapper mapper, string serviceName)
            : base(dbContext, mapper, serviceName)
        {

        }

        public async Task<TModel> Update(long id, TModel model)
        {
            return await _dbContext.Update<TEntity, TModel>(model, id, _mapper);
        }

        public virtual async Task<TModel> Delete(long id)
        {
            return await _dbContext.Delete<TEntity, TModel>(id, _mapper, OnDeleting);
        }

        public async Task<TModel> Restore(long id)
        {
            return await _dbContext.Restore<TEntity, TModel>(id, _mapper);
        }

        public async Task<PageListModel<TModel>> GetAll(int page, int count)
        {
            return await _dbContext.GetAll<TEntity, TModel>(page, count, _mapper);
        }

        public async Task<TModel> GetOne(long id)
        {
            return await _dbContext.GetOne<TEntity, TModel>(id, _mapper, null, null);
        }

        protected async Task<TModel> ChangeEntity(long id, Action<TEntity> entityAction)
        {
            var entity = await _dbContext.GetEntityForUpdate<TEntity>(id);
            entityAction(entity);

            await _dbContext.SaveChangesAsync();
            return _mapper.Map<TModel>(entity);
        }

        protected override void OnAdding(TEntity entity)
        {
            entity.IsDeleted = false;
            entity.LastUpdated = DateTime.UtcNow;
        }

        /// <summary>
        /// Валидация модели коллекции перед созданием коллекции
        /// </summary>
        /// <param name="model">Модель коллекции</param>
        protected virtual void ValidateCreate(TModel model)
        {
            model.ValidateNotNull();
        }

        /// <summary>
        /// Валидация модели коллекции перед изменением коллекции
        /// </summary>
        /// <param name="model">DTO с типом уведомления</param>
        protected virtual void ValidateUpdate(TModel model)
        {
            model.ValidateNotNull();
        }
    }
}
