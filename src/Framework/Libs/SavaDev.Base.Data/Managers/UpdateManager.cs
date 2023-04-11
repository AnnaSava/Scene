using AutoMapper;
using Microsoft.Extensions.Logging;
using SavaDev.Base.Data.Context;
using SavaDev.Base.Data.Entities.Interfaces;
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
    public class UpdateManager<TKey, TEntity, TFormModel>
        where TEntity : class, IEntity<TKey>, new()
        where TFormModel : IFormModel
    {
        private readonly IMapper _mapper;
        private readonly IDbContext _dbContext;
        private readonly ILogger _logger;

        public UpdateManager(IDbContext dbContext, IMapper mapper, ILogger logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<OperationResult> Update(TKey id, TFormModel model, Func<TEntity, Task>? validateEntity = null)
        {
            var updater = new EntityUpdater<TKey, TEntity>(_dbContext, _logger)
                .ValidateModel(async (model) => { })
                .GetEntity(async (id) => await _dbContext.GetEntityForChange<TKey, TEntity>(id))
                .ValidateEntity(validateEntity)
                .SetValues(async (entity, model) => _mapper.Map(model, entity))
                .Update(DoUpdate)
                .SuccessResult(entity => new OperationResult(_mapper.Map<TFormModel>(entity)));

            return await updater.DoUpdate<TFormModel>(id, model);
        }

        public async Task<OperationResult> Restore(TKey id, Func<TEntity, bool> fieldFunc, Func<TEntity, Task>? validateEntity = null)
        {
            var updater = new EntityUpdater<TKey, TEntity>(_dbContext, _logger)
                .GetDeletedEntity(async (id) =>
                {
                    var entity = await _dbContext.GetEntityForChange<TKey, TEntity>(id, restore: true);
                    return entity;
                })
                .ValidateEntity(validateEntity)
                .SetValues(async (entity) => fieldFunc(entity))
                .Update(DoUpdate)
                .SuccessResult(entity => new OperationResult(1, id));

            return await updater.DoUpdate(id, restore: true);
        }

        public async Task<OperationResult> SetField(TKey id, Func<TEntity, bool> fieldFunc)
        {
            var updater = new EntityUpdater<TKey, TEntity>(_dbContext, _logger)
                .GetEntity(async (id) => await _dbContext.GetEntityForChange<TKey, TEntity>(id))
                .SetValues(async (entity) => fieldFunc(entity))
                .Update(DoUpdate)
                .SuccessResult(entity => new OperationResult(1, id));

            return await updater.DoUpdate(id);
        }

        public async Task<OperationResult> SetField(TKey id, Func<TEntity, string> fieldFunc)
        {
            var updater = new EntityUpdater<TKey, TEntity>(_dbContext, _logger)
                .GetEntity(async (id) => await _dbContext.GetEntityForChange<TKey, TEntity>(id))
                .SetValues(async (entity) => fieldFunc(entity))
                .Update(DoUpdate)
                .SuccessResult(entity => new OperationResult(1, id));

            return await updater.DoUpdate(id);
        }

        public async Task<OperationResult> SetField(TKey id, Func<TEntity, bool> fieldFunc, Func<TEntity, Task> validateEntity)
        {
            var updater = new EntityUpdater<TKey, TEntity>(_dbContext, _logger)
                .GetEntity(async (id) => await _dbContext.GetEntityForChange<TKey, TEntity>(id))
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
