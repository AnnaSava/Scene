﻿using AutoMapper;
using SavaDev.Base.Data.Services;
using SavaDev.Base.Data.Services.Interfaces;
using SavaDev.Base.Front.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Front.Services
{
    public class UpdateViewManager<TKey, TFormModel>
        where TFormModel : class
    {
        protected readonly IEntityEditService<TKey, TFormModel> _entityService;
        protected readonly IMapper _mapper;

        public Func<TKey, Task<bool>>? CheckAccess { get; set; }
        public Action<TFormModel>? SetValues { get; set; }
        public Func<TFormModel, TFormModel, Task<OperationResult>>? ProcessDrafts { get; set; }
        public Func<TFormModel, Task<OperationResult>>? ProcessVersion { get; set; }

        public UpdateViewManager(IEntityEditService<TKey, TFormModel> entityService,
            IMapper mapper)
        {
            _entityService = entityService;
            _mapper = mapper;
        }

        public async Task<OperationResult> Update(TKey id, object model)
        {
            if(model == null)
                throw new ArgumentNullException("model");

            if (CheckAccess == null)
            {
                throw new NotPermittedException();
            }
            var can = await CheckAccess.Invoke(id);
            if (!can)
            {
                throw new NotPermittedException();
            }

            var curModel = _mapper.Map<TFormModel>(model);
            if (model == null)
                throw new InvalidOperationException("newModel is null");
            SetValues?.Invoke(curModel);

            var result = await _entityService.Update(id, curModel);

            if (!result.IsSuccess)
            {
                throw new Exception($"Creating an object of type {model.GetType().Name} failed");
            }

            var resultModel = result.ProcessedObject as TFormModel;
            if (resultModel == null)
            {
                throw new Exception("ResultModel is null");
            }

            if (ProcessDrafts != null)
            {
                await ProcessDrafts.Invoke(curModel, resultModel);
            }
            if (ProcessVersion != null)
            {
                await ProcessVersion.Invoke(resultModel);
            }

            return result;
        }
    }
}
