using AutoMapper;
using Framework.Base.DataService.Contract.Interfaces;
using Framework.Base.DataService.Contract.Models;
using Framework.Base.DataService.Entities;
using Framework.Base.DataService.Exceptions;
using Framework.Base.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Framework.Base.DataService.Services
{
    public abstract class BaseAliasedDbService<TEntity, TModel> : BaseDbService<TEntity, TModel>, IBaseAliasedDbService<TModel>
        where TEntity : BaseAliasedEntity<long>
        where TModel : BaseAliasedModel<long>
    {
        public BaseAliasedDbService(DbContext dbContext, IMapper mapper, string serviceName)
            : base(dbContext, mapper, serviceName)
        {

        }

        public async Task<TModel> GetOneByAlias(string alias)
        {
            var entity = await GetEntityByAlias(alias);

            return _mapper.Map<TModel>(entity);
        }

        /// <summary>
        /// Поиск сущности коллекции по алиасу
        /// </summary>
        /// <param name="alias">Алиас коллекции</param>
        /// <returns>Возвращает Task of <see cref="TEntity"/></returns>
        private async Task<TEntity> GetEntityByAlias(string alias)
        {
            if (string.IsNullOrEmpty(alias))
                throw new ProjectArgumentException(
                    GetType(),
                    nameof(GetEntityByAlias),
                    nameof(alias),
                    alias);

            var entity = await _dbContext.Set<TEntity>()
                .FirstOrDefaultAsync(m => m.Alias == alias);

            return entity ?? throw new EntityNotFoundException(
                       GetType(),
                       nameof(GetEntityByAlias),
                       typeof(TEntity).FullName,
                       alias);
        }

        protected override void ValidateCreate(TModel model)
        {
            base.ValidateCreate(model);

            if (string.IsNullOrWhiteSpace(model.Alias))
                throw new ProjectArgumentException(
                    GetType(),
                    nameof(ValidateCreate),
                    nameof(model.Alias),
                    model.Alias);
        }

        protected override void ValidateUpdate(TModel model)
        {
            base.ValidateUpdate(model);

            if (string.IsNullOrWhiteSpace(model.Alias))
                throw new ProjectArgumentException(
                    GetType(),
                    nameof(ValidateCreate),
                    nameof(model.Alias),
                    model.Alias);
        }
    }
}
