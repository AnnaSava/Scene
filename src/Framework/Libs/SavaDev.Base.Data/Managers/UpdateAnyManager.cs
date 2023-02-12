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
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Data.Managers
{
    public class UpdateAnyManager<TEntity, TFormModel>
        where TEntity : class, new()
        where TFormModel: IFormModel
    {
        private readonly IMapper _mapper;
        private readonly IDbContext _dbContext;
        private readonly ILogger _logger;
        private IChangeAnySelector<TEntity> updateSelector;

        public UpdateAnyManager(IDbContext dbContext, IMapper mapper, ILogger logger, IChangeAnySelector<TEntity> selector)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            updateSelector = selector;
        }

        public async Task<OperationResult> Update(Expression<Func<TEntity, bool>> expression, TFormModel model)
        {
            var updater = new EntityUpdater<bool, TEntity>(_dbContext, _logger) // TODO придумать все же что-то с параметром айдишника
                .ValidateModel(async (model) =>
                {

                })
                .GetEntity(async (exp) => await updateSelector.GetEntityForChange(exp))
                .SetValues(async (entity, model) => _mapper.Map(model, entity))
                .Update(DoUpdate)
                .SuccessResult(entity => new OperationResult(1, _mapper.Map<TFormModel>(entity)))
                .ErrorResult((id, errMessage) => new OperationResult((int)DbOperationRows.OnFailure, id, new OperationExceptionInfo(errMessage)));

            return await updater.DoUpdate<TFormModel>(expression, model);
        }

        private async Task<OperationResult> DoUpdate(TEntity entity)
        {
            var rows = await _dbContext.SaveChangesAsync();
            return new OperationResult(rows);
        }
    }
}
