using AutoMapper;
using SavaDev.Base.Data.Services;
using SavaDev.Base.Data.Services.Interfaces;
using SavaDev.Base.Front.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Front.Services
{
    public class CreateViewManager<TFormModel>
        where TFormModel : class
    {
        protected readonly IEntityCrudService<TFormModel> _entityService;
        protected readonly IMapper _mapper;

        public Func<Task<bool>>? CheckAccess { get; set; }
        public Action<TFormModel>? SetValues { get; set; }
        public Func<TFormModel, Task<OperationResult>>? ProcessCommunity { get; set; }
        public Func<TFormModel, TFormModel, Task<OperationResult>>? ProcessDrafts { get; set; }
        public Func<TFormModel, Task<OperationResult>> ProcessVersion { get; set; }

        public CreateViewManager(IEntityCrudService<TFormModel> entityService,
            IMapper mapper)
        {
            _entityService = entityService;
            _mapper = mapper;
        }

        public async Task<OperationResult> Create(object model)
        {
            var can = await CheckAccess?.Invoke();
            if (!can)
            {
                throw new NotPermittedException();
            }

            var newModel = _mapper.Map<TFormModel>(model);
            SetValues?.Invoke(newModel);

            var result = await _entityService.Create(newModel);

            if (!result.IsSuccess)
            {
                throw new Exception($"Creating an object of type {model.GetType().Name} failed");
            }

            var resultModel = result.ProcessedObject as TFormModel;
            if (resultModel == null)
            {
                throw new Exception("ResultModel is null");
            }
            await ProcessCommunity?.Invoke(resultModel);
            await ProcessDrafts?.Invoke(newModel, resultModel);
            await ProcessVersion?.Invoke(resultModel);

            return result;
        }
    }
}
