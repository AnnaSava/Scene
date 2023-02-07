using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SavaDev.Base.Data.Context;
using SavaDev.Base.Data.Entities.Interfaces;
using SavaDev.Base.Data.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Data.Managers
{
    public class OneRestorableSelector<TKey, TEntity>
        where TEntity : class, IEntityRestorable, IEntity<TKey>
    {
        private readonly IMapper _mapper;
        private readonly IDbContext _dbContext;
        private readonly ILogger _logger;

        public OneRestorableSelector(IDbContext dbContext, IMapper mapper, ILogger logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<TModel?> GetOne<TModel>(Expression<Func<TEntity, bool>> expression)
           where TModel : IAnyModel
        {
            return await new OneSelector<TEntity>(_dbContext, _mapper, _logger).GetOne<TModel>(expression);
        }

        public async Task<TModel> GetOne<TModel>(TKey id)
        {
            var elQuery = _dbContext.Set<TEntity>()
                .Where(m => m.Id.Equals(id) && m.IsDeleted == false);

            return await elQuery.ProjectTo<TModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<TModel> GetOne<TModel>(TKey id, Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeFunc)
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

        public async Task<bool> CheckExists(Expression<Func<TEntity, bool>> expression)
        {
            return await _dbContext.Set<TEntity>().AnyAsync(expression);
        }
    }
}
