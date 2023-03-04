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
    public class CreateManager<TEntity>
        where TEntity : class, IAnyEntity        
    {
        private readonly IMapper _mapper;
        private readonly IDbContext _dbContext;
        private readonly ILogger _logger;

        private readonly ServiceInftrastructure _infra;

        public CreateManager(IDbContext dbContext, IMapper mapper, ILogger logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public CreateManager(ServiceInftrastructure infrastructure)
        {
            _infra = infrastructure;
            _dbContext = _infra.DbContext;
            _mapper = _infra.Mapper;
            _logger = _infra.Logger;
        }

        public async Task<OperationResult> Create<TFormModel>(TFormModel model, Func<TEntity, Task<TEntity>> setValues)
            where TFormModel : IFormModel
        {
            var creator = new EntityCreator<TEntity>(_dbContext, _logger)
                .ValidateModel(async (model) => { })
                .SetValues(async (model) => { var entity = _mapper.Map<TEntity>(model); setValues(entity); return entity; })
                .Create(DoCreate)
                .SuccessResult(entity => new OperationResult(1, _mapper.Map<TFormModel>(entity)));

            return await creator.Create(model);
        }

        public async Task<OperationResult> Create<TFormModel>(TFormModel model, Func<IFormModel, Task> validate, Action<TEntity> setEntityValues)
            where TFormModel : IFormModel
        {
            var creator = new EntityCreator<TEntity>(_dbContext, _logger)
                .ValidateModel(validate)
                .SetValues(async (model) => { var entity = _mapper.Map<TEntity>(model); setEntityValues(entity); return entity; })
                .Create(DoCreate)
                .SuccessResult(entity => new OperationResult(1, _mapper.Map<TFormModel>(entity)));

            return await creator.Create(model);
        }

        private async Task<OperationResult> DoCreate(TEntity entity)
        {
            var addResult = await _dbContext.AddAsync(entity);
            var rows = await _dbContext.SaveChangesAsync();
            return new OperationResult(rows); // не уверена, что так красиво
        }
    }

    public class CreateManager<TEntity, TFormModel>
        where TEntity : class, IAnyEntity
        where TFormModel : IFormModel
    {
        private readonly IMapper _mapper;
        private readonly IDbContext _dbContext;
        private readonly ILogger _logger;

        private readonly ServiceInftrastructure _infra;

        public CreateManager(IDbContext dbContext, IMapper mapper, ILogger logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public CreateManager(ServiceInftrastructure infrastructure)
        {
            _infra = infrastructure;
            _dbContext = _infra.DbContext;
            _mapper = _infra.Mapper;
            _logger = _infra.Logger;
        }

        public async Task<OperationResult> Create(TFormModel model, 
            Func<IFormModel, Task>? validate = null, 
            Action<TEntity>? setValues = null)
        {
            var creator = new EntityCreator<TEntity>(_dbContext, _logger)
                .ValidateModel(validate)
                .SetValues(async (model) => 
                { 
                    var entity = _mapper.Map<TEntity>(model); 
                    setValues?.Invoke(entity); 
                    return entity; 
                })
                .Create(DoCreate)
                .SuccessResult(entity => new OperationResult(_mapper.Map<TFormModel>(entity)));

            return await creator.Create(model);
        }

        private async Task<OperationResult> DoCreate(TEntity entity)
        {
            var addResult = await _dbContext.Set<TEntity>().AddAsync(entity);
            var rows = await _dbContext.SaveChangesAsync();
            return new OperationResult(rows); // не уверена, что так красиво
        }
    }
}
