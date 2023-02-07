﻿using AutoMapper;
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
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Data.Managers
{
    public class RemoveManager<TEntity> 
        where TEntity: class, IAnyEntity
    {
        private readonly IMapper _mapper;
        private readonly IDbContext _dbContext;
        private readonly ILogger _logger;
        private IUpdateAnySelector<TEntity> updateSelector;

        public RemoveManager(IDbContext dbContext, IMapper mapper, ILogger logger,IUpdateAnySelector<TEntity> selector)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            updateSelector = selector;
        }

        public async Task<OperationResult> Remove(Expression<Func<TEntity, bool>> expression)
        {
            var remover = new EntityRemover<bool, TEntity>(_dbContext, _logger) // TODO придумать все же что-то с параметром айдишника
                .GetEntity(async (exp) => await updateSelector.GetEntityForUpdate(exp))
                .Remove(DoRemove)
                .SuccessResult(entity => new OperationResult(1))
                .ErrorResult((errMessage) => new OperationResult((int)DbOperationRows.OnFailure, new OperationExceptionInfo(errMessage)));

            return await remover.DoRemove(expression);
        }

        private async Task<OperationResult> DoRemove(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            var rows = await _dbContext.SaveChangesAsync();
            return new OperationResult(rows);
        }
    }
}
