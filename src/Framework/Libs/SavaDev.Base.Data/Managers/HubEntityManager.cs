using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SavaDev.Base.Data.Context;
using SavaDev.Base.Data.Entities;
using SavaDev.Base.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Data.Managers
{
    public class HubEntityManager<TKey, TEntity, TFormModel>
         where TEntity : BaseHubEntity, new()
    {
        private readonly IMapper _mapper;
        private readonly IDbContext _dbContext;
        private readonly ILogger _logger;

        public HubEntityManager(IDbContext dbContext, IMapper mapper, ILogger logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<OperationResult<TFormModel>> Create(TFormModel model, Action<TEntity> onCreating = null, Action<TEntity> onCreated = null)
        {
            using var tran = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var newEntity = _mapper.Map<TEntity>(model);
                newEntity.Id = Guid.NewGuid();

                var addResult = await _dbContext.AddAsync(newEntity);

                onCreating?.Invoke(newEntity);
                var rows = await _dbContext.SaveChangesAsync();
                onCreated?.Invoke(addResult.Entity);

                var result = new OperationResult<TFormModel>(rows, _mapper.Map<TFormModel>(addResult.Entity));

                await tran.CommitAsync();
                return result;
            }
            catch (Exception ex)
            {
                await tran.RollbackAsync();
                _logger.LogError($"{nameof(Create)}: {ex.Message} {ex.StackTrace}");
                var result = new OperationResult<TFormModel>(0, model, new OperationExceptionInfo(ex.Message));
                return result;
            }
        }

        // TODO возможно, include тут не нужен. Вроде как этапы и чекпойнты выбираются вместе с целью и так.
        public async Task<TModel> GetOne<TModel>(Guid id, Func<IQueryable<TEntity>, IQueryable<TEntity>> includeFunc = null)
        {
            var elQuery = _dbContext.Set<TEntity>()
                .Where(m => m.Id.Equals(id));

            if (includeFunc != null)
            {
                elQuery = includeFunc(elQuery);
            }

            return await elQuery.ProjectTo<TModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }
    }
}
