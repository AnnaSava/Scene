using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SavaDev.Base.Data.Context;
using SavaDev.Base.Data.Entities.Interfaces;
using SavaDev.Base.Data.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Data.Managers
{
    public class RestoreSelector<TKey, TEntity> : IUpdateSelector<TKey, TEntity>
        where TEntity : class, IEntity<TKey>, IEntityRestorable
    {
        private readonly IMapper _mapper;
        private readonly IDbContext _dbContext;
        private readonly ILogger _logger;

        public RestoreSelector(IDbContext dbContext, IMapper mapper, ILogger logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<TEntity> GetEntityForUpdate(TKey id)
        {
            var entity = await _dbContext.Set<TEntity>().FirstOrDefaultAsync(m => m.Id.Equals(id) && m.IsDeleted == true);

            if (entity == null)
                throw new EntityNotFoundException();

            entity.LastUpdated = DateTime.UtcNow;

            return entity;
        }

        public async Task<TEntity> GetEntityForUpdate(TKey id, Func<IQueryable<TEntity>, IQueryable<TEntity>> include)
        {
            throw new NotImplementedException();
        }
    }
}
