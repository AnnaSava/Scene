using AutoMapper;
using Framework.Base.DataService.Contract.Interfaces;
using Framework.Base.DataService.Entities;
using Framework.Base.Exceptions;
using Framework.Base.Types.ModelTypes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Framework.Base.DataService.Services
{
    public abstract class BaseEntityService<TEntity, TModel>
        where TEntity : class, IAnyEntity
        where TModel : IAnyModel
    {
        protected readonly IDbContext _dbContext;
        protected readonly IMapper _mapper;

        public BaseEntityService(IDbContext dbContext, IMapper mapper, string serviceName)
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

        public virtual async Task<TModel> Create(TModel model)
        {
            return await _dbContext.Create<TEntity, TModel>(model, _mapper, OnAdding);
        }  

        // prepare model
        protected virtual void OnAdding(TEntity entity) { }
        protected virtual void OnDeleting(TEntity entity) { }
        protected virtual void OnRestoring(TEntity entity) { }
    }
}
