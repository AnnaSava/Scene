using AutoMapper;
using AutoMapper.QueryableExtensions;
using Framework.Base.DataService.Contract.Interfaces;
using Framework.Base.DataService.Entities;
using Framework.Base.DataService.Exceptions;
using Framework.Base.Types.ModelTypes;
using Framework.Base.Types.View;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Base.DataService.Services.Managers
{
    public class AnyEntityManager<TEntity, TFormModel>
        where TEntity : class, IAnyEntity
    {
        private readonly IMapper _mapper;
        private readonly IDbContext _dbContext;
        private readonly ILogger _logger;

        public AnyEntityManager(IDbContext dbContext, IMapper mapper, ILogger logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        // TODO сделать по аналогии с другими менеджерами
        public async Task<OperationResult<TFormModel>> Create(TFormModel model)
        {
            var newEntity = _mapper.Map<TEntity>(model);
            // OnAdding?.Invoke(newEntity);
            var addResult = await _dbContext.AddAsync(newEntity);
            var rows = await _dbContext.SaveChangesAsync();

            return new OperationResult<TFormModel>(rows, _mapper.Map<TFormModel>(addResult.Entity));
        }

        public async Task<OperationResult<TFormModel>> Update(Expression<Func<TEntity, bool>> expression, TFormModel model)
        {
            var currentEntity = await _dbContext.Set<TEntity>().FirstOrDefaultAsync(expression);
            if (currentEntity == null)
                throw new EntityNotFoundException();

            _mapper.Map(model, currentEntity);
            var rows = await _dbContext.SaveChangesAsync();

            return new OperationResult<TFormModel>(rows, _mapper.Map<TFormModel>(currentEntity));
        }

        public async Task<OperationResult> Remove(Expression<Func<TEntity, bool>> expression)
        {
            var entity = await _dbContext.GetEntityForRemove(expression);
            _dbContext.Set<TEntity>().Remove(entity);
            var rows = await _dbContext.SaveChangesAsync();

            return new OperationResult(rows);
        }

        public async Task<TModel> GetOne<TModel>(Expression<Func<TEntity, bool>> expression)
            where TModel : IAnyModel
        {
            var elQuery = _dbContext.Set<TEntity>()
                .Where(expression);
            return await elQuery.ProjectTo<TModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> CheckExists(Expression<Func<TEntity, bool>> expression)
        {
            return await _dbContext.Set<TEntity>().AnyAsync(expression);
        }
    }
}
