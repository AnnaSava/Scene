using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SavaDev.Base.Data.Context;
using SavaDev.Base.Data.Models.Interfaces;
using SavaDev.Base.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Data.Managers
{
    public class OneSelector<TEntity>
        where TEntity : class
    {
        private readonly IMapper _mapper;
        private readonly IDbContext _dbContext;
        private readonly ILogger _logger;

        private readonly ServiceInftrastructure _infra;

        public OneSelector(IDbContext dbContext, IMapper mapper, ILogger logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public OneSelector(ServiceInftrastructure infrastructure)
        {
            _infra = infrastructure;
            _dbContext = _infra.DbContext;
            _mapper = _infra.Mapper;
            _logger = _infra.Logger;
        }


        public async Task<TModel?> GetOne<TModel>(Expression<Func<TEntity, bool>> expression)
           where TModel : IAnyModel
        {
            var elQuery = _dbContext.Set<TEntity>()
                .Where(expression);
            return await elQuery.ProjectTo<TModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        // TODO возможно, include тут не нужен. Вроде как этапы и чекпойнты выбираются вместе с целью и так.
        public async Task<TModel> GetOne<TModel>(Guid id, Func<IQueryable<TEntity>, IQueryable<TEntity>> includeFunc = null)
        {
            throw new NotImplementedException();
            //var elQuery = _dbContext.Set<TEntity>()
            //    .Where(m => m.Id.Equals(id));

            //if (includeFunc != null)
            //{
            //    elQuery = includeFunc(elQuery);
            //}

            //return await elQuery.ProjectTo<TModel>(_mapper.ConfigurationProvider)
            //    .FirstOrDefaultAsync();
        }

        public async Task<bool> CheckExists(Expression<Func<TEntity, bool>> expression)
        {
            return await _dbContext.Set<TEntity>().AnyAsync(expression);
        }
    }
}
