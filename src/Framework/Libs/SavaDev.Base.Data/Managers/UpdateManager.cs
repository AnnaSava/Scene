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
        private IChangeSelector<TKey, TEntity> updateSelector;

        public UpdateManager(IDbContext dbContext, IMapper mapper, ILogger logger, IChangeSelector<TKey, TEntity> selector)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            updateSelector = selector;
        }

        public async Task<OperationResult> Update(TKey id, TFormModel model)
        {
            var updater = new EntityUpdater<TKey, TEntity>(_dbContext, _logger)
                .ValidateModel(async (model) => { })
                .GetEntity(async (id) => await updateSelector.GetEntityForChange(id))
                .SetValues(async (entity, model) => _mapper.Map(model, entity))
                .Update(DoUpdate)
                .SuccessResult(entity => new OperationResult(1, _mapper.Map<TFormModel>(entity)))
                .ErrorResult((id, errMessage) => new OperationResult(DbOperationRows.OnFailure, id, new OperationExceptionInfo(errMessage)));

            return await updater.DoUpdate<TFormModel>(id, model);
        }

        public async Task<OperationResult> Update(TKey id, TFormModel model, Func<TEntity, Task> validateEntity)
        {
            var updater = new EntityUpdater<TKey, TEntity>(_dbContext, _logger)
                .ValidateModel(async (model) => { })
                .GetEntity(async (id) => await updateSelector.GetEntityForChange(id))
                .ValidateEntity(validateEntity)
                .SetValues(async (entity, model) => _mapper.Map(model, entity))
                .Update(DoUpdate)
                .SuccessResult(entity => new OperationResult(1, _mapper.Map<TFormModel>(entity)))
                .ErrorResult((id, errMessage) => new OperationResult(DbOperationRows.OnFailure, id, new OperationExceptionInfo(errMessage)));

            return await updater.DoUpdate<TFormModel>(id, model);
        }

        private async Task<OperationResult> DoUpdate(TEntity entity)
        {
            var rows = await _dbContext.SaveChangesAsync();
            return new OperationResult(rows);
        }
    }
}
