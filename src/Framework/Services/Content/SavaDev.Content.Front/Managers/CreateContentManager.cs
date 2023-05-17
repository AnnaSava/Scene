using AutoMapper;
using SavaDev.Base.Data.Services;
using SavaDev.Base.Data.Services.Interfaces;
using SavaDev.Base.Front.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Content.Front.Managers
{
    public class CreateContentManager<TFormModel>
        where TFormModel : class
    {
        protected readonly IEntityCreateService<TFormModel> _entityService;
        protected readonly IMapper _mapper;

        public Func<Task<bool>>? CheckAccess { get; set; }
        public Func<TFormModel, Task<IEnumerable<ValidationResult>>>? Validate { get; set; }
        public Action<TFormModel>? SetValues { get; set; }
        public Func<TFormModel, Task<OperationResult>>? ProcessGroup { get; set; }
        public Func<TFormModel, TFormModel, Task<OperationResult>>? ProcessDrafts { get; set; }
        public Func<TFormModel, Task<OperationResult>>? ProcessVersion { get; set; }

        public CreateContentManager(IEntityCreateService<TFormModel> entityService,
            IMapper mapper)
        {
            _entityService = entityService;
            _mapper = mapper;
        }

        public async Task<OperationResult> Create(object model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            if (CheckAccess == null)
            {
                throw new NotPermittedException();
            }
            var can = await CheckAccess.Invoke();
            if (!can)
            {
                throw new NotPermittedException();
            }            

            var newModel = _mapper.Map<TFormModel>(model);
            if (model == null)
                throw new InvalidOperationException("newModel is null");
            SetValues?.Invoke(newModel);

            if (Validate == null)
                throw new InvalidOperationException();
            var validationErrors = await Validate.Invoke(newModel);

            if (validationErrors != null && validationErrors.Any())
            {
                // TODO что-то делать с ошибками валидации
                return new OperationResult(0);
            }

            var result = await _entityService.Create(newModel);

            if (!result.IsSuccess)
            {
                throw new Exception($"Creating an object of type {model.GetType().Name} failed");
            }

            var resultModel = result.GetProcessedObject<TFormModel>();
            if (resultModel == null)
            {
                throw new Exception("ResultModel is null");
            }

            if (ProcessGroup != null)
            {
                await ProcessGroup.Invoke(resultModel);
            }
            if (ProcessDrafts != null)
            {
                await ProcessDrafts.Invoke(newModel, resultModel);
            }
            if (ProcessVersion != null)
            {
                await ProcessVersion.Invoke(resultModel);
            }

            return result;
        }
    }
}
