using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SavaDev.Base.Data.Context;
using SavaDev.Base.Data.Entities.Interfaces;
using SavaDev.Base.Data.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Data.Managers
{
    public class UpdateAnySelector<TEntity> : IChangeAnySelector<TEntity>
        where TEntity: class, IAnyEntity, new()
    {
        private readonly IMapper _mapper;
        private readonly IDbContext _dbContext;
        private readonly ILogger _logger;

        public UpdateAnySelector(IDbContext dbContext, IMapper mapper, ILogger logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<TEntity> GetEntityForChange(Expression<Func<TEntity, bool>> expression)
        {
            var entity = await _dbContext.Set<TEntity>().FirstOrDefaultAsync(expression);

            if (entity == null)
                throw new EntityNotFoundException();

            return entity;
        }
    }
}
