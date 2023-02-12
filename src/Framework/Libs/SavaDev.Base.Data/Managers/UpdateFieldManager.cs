using AutoMapper;
using Microsoft.Extensions.Logging;
using SavaDev.Base.Data.Context;
using SavaDev.Base.Data.Enums;
using SavaDev.Base.Data.Managers.Crud;
using SavaDev.Base.Data.Models.Interfaces;
using SavaDev.Base.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Data.Managers
{
    public class UpdateFieldManager<TKey, TEntity>
        where TEntity : class, new()
    {
        private readonly IMapper _mapper;
        private readonly IDbContext _dbContext;
        private readonly ILogger _logger;
        private IChangeSelector<TKey, TEntity> SelectorManager;

        public UpdateFieldManager(IDbContext dbContext, IMapper mapper, ILogger logger, IChangeSelector<TKey, TEntity> selector)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            SelectorManager = selector;
        }

        public async Task<OperationResult> SetField(TKey id, Func<TEntity, bool> fieldFunc)
        {
            var updater = new EntityUpdater<TKey, TEntity>(_dbContext, _logger)
                .GetEntity(async (id) => await SelectorManager.GetEntityForChange(id))
                .SetValues(async (entity) => fieldFunc(entity))
                .Update(DoUpdate)
                .SuccessResult(entity => new OperationResult(1, id));

            return await updater.DoUpdate(id);
        }

        public async Task<OperationResult> SetField(TKey id, Func<TEntity, string> fieldFunc)
        {
            var updater = new EntityUpdater<TKey, TEntity>(_dbContext, _logger)
                .GetEntity(async (id) => await SelectorManager.GetEntityForChange(id))
                .SetValues(async (entity) => fieldFunc(entity))
                .Update(DoUpdate)
                .SuccessResult(entity => new OperationResult(1, id));

            return await updater.DoUpdate(id);
        }

        public async Task<OperationResult> SetField(TKey id, Func<TEntity, bool> fieldFunc, Func<TEntity, Task> validateEntity)
        {
            var updater = new EntityUpdater<TKey, TEntity>(_dbContext, _logger)
                .GetEntity(async (id) => await SelectorManager.GetEntityForChange(id))
                .ValidateEntity(validateEntity)
                .SetValues(async (entity) => fieldFunc(entity))
                .Update(DoUpdate)
                .SuccessResult(entity => new OperationResult(1, id));

            return await updater.DoUpdate(id);
        }

        private async Task<OperationResult> DoUpdate(TEntity entity)
        {
            var rows = await _dbContext.SaveChangesAsync();
            return new OperationResult(rows);
        }
    }
}
