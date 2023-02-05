using AutoMapper;
using SavaDev.Base.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Front.Services
{
    public class CrudManager<TFormModel, TFormViewModel>
    {
        protected readonly IEntityCrudService<TFormModel> _entityService;
        protected readonly IMapper _mapper;

        public CrudManager(IEntityCrudService<TFormModel> entityService,
            IMapper mapper)
        {
            _entityService = entityService;
            _mapper = mapper;
        }

        public async Task<OperationResult> Create(TFormViewModel model)
        {
            //model.Permissions = (await _permissionService.FilterExisting(model.Permissions)).ToList();

            var newModel = _mapper.Map<TFormModel>(model);
            var resultModel = await _entityService.Create(newModel);
            return new OperationResult(0, _mapper.Map<TFormViewModel>(resultModel));
        }

        public async Task<OperationResult> Update(long id, TFormViewModel model)
        {
           // model.Permissions = (await _permissionService.FilterExisting(model.Permissions)).ToList();

            var newModel = _mapper.Map<TFormModel>(model);
          //  newModel.Id = id;
            var resultModel = await _entityService.Update(id, newModel);
            return new OperationResult(0, _mapper.Map<TFormViewModel>(resultModel));
        }

        public async Task<OperationResult> Delete(long id)
        {
            var resultModel = await _entityService.Delete(id);
            return new OperationResult(0, _mapper.Map<TFormViewModel>(resultModel));
        }

        public async Task<OperationResult> Restore(long id)
        {
            var resultModel = await _entityService.Restore(id);
            return new OperationResult(0, _mapper.Map<TFormViewModel>(resultModel));
        }
    }
}
