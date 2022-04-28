using AutoMapper;
using Framework.Base.DataService.Contract.Interfaces;
using Framework.Base.DataService.Entities;
using Framework.Base.Exceptions;
using Framework.Base.Types.ModelTypes;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Framework.Base.DataService.Services
{
    public class AnyEntityService<TEntity, TModel> : IAnyEntityService<TModel>
        where TEntity : class, IAnyEntity
        where TModel : IAnyModel
    {
        protected readonly IDbContext _dbContext;
        protected readonly IMapper _mapper;

        public AnyEntityService(IDbContext dbContext, IMapper mapper, string serviceName)
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
            return await _dbContext.Create<TEntity, TModel>(model, _mapper, OnAdding);
        }

        // prepare model
        protected virtual void OnAdding(TEntity entity) { }

        protected async Task<TModel> Update(TModel model, Expression<Func<TEntity, bool>> expression)
        {
            return await _dbContext.Update(model, expression, _mapper);
        }

        protected async Task Remove(Expression<Func<TEntity, bool>> expression)
        {
            await _dbContext.Remove(expression);
        }

        protected async Task<TModel> GetOne(Expression<Func<TEntity, bool>> expression)
        {
            return await _dbContext.GetOne<TEntity, TModel>(expression, _mapper);
        }
    }
}
