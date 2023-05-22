using AutoMapper;
using SavaDev.Base.Data.Registry;
using SavaDev.Base.Front.Registry;
using SavaDev.Base.Front.Services;
using SavaDev.General.Data.Contract;
using SavaDev.General.Data.Contract.Models;
using SavaDev.General.Front.Contract;
using SavaDev.General.Front.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.General.Front.Services
{
    public class LegalDocumentViewService : ILegalDocumentViewService
    {
        private readonly ILegalDocumentService _legalDocumentService;
        private readonly IMapper _mapper;

        public LegalDocumentViewService(ILegalDocumentService legalDocumentDbService, IMapper mapper)
        {
            _legalDocumentService = legalDocumentDbService;
            _mapper = mapper;
        }

        public async Task<TResult> GetOne<TResult>(long id)
        {
            var model = await _legalDocumentService.GetOne<LegalDocumentModel>(id);
            return _mapper.Map<TResult>(model);
        }

        public async Task<TResult> GetActual<TResult>(string permName, string culture)
        {
            var model = await _legalDocumentService.GetActual<LegalDocumentModel>(permName, culture);
            return _mapper.Map<TResult>(model);
        }

        public async Task<LegalDocumentViewModel> Create(LegalDocumentFormViewModel model)
        {
            var entity = _mapper.Map<LegalDocumentModel>(model);
            entity.Culture = entity.Culture.ToLower();
            var created = await _legalDocumentService.Create(entity);
            return _mapper.Map<LegalDocumentViewModel>(created.GetProcessedObject());
        }

        public async Task<LegalDocumentViewModel> CreateTranslation(LegalDocumentFormViewModel model)
        {
            var entity = _mapper.Map<LegalDocumentModel>(model);
            entity.Culture = entity.Culture.ToLower();
            var created = await _legalDocumentService.CreateTranslation(entity);
            return _mapper.Map<LegalDocumentViewModel>(created.GetProcessedObject());
        }

        public async Task<LegalDocumentViewModel> Update(long id, LegalDocumentFormViewModel model)
        {
            var newModel = _mapper.Map<LegalDocumentModel>(model);
            newModel.Id = id;
            var resultModel = await _legalDocumentService.Update(id, newModel);
            return _mapper.Map<LegalDocumentViewModel>(resultModel.GetProcessedObject());
        }

        public async Task Publish(long id)
        {
            await _legalDocumentService.Publish(id);
        }

        public async Task<LegalDocumentViewModel> CreateVersion(LegalDocumentFormViewModel model)
        {
            var entity = _mapper.Map<LegalDocumentModel>(model);
            entity.Culture = entity.Culture.ToLower();
            var created = await _legalDocumentService.CreateVersion(entity);
            return _mapper.Map<LegalDocumentViewModel>(created.GetProcessedObject());
        }

        public async Task<LegalDocumentViewModel> Delete(long id)
        {
            var resultModel = await _legalDocumentService.Delete(id);
            return _mapper.Map<LegalDocumentViewModel>(resultModel);
        }

        public async Task<LegalDocumentViewModel> Restore(long id)
        {
            var resultModel = await _legalDocumentService.Restore(id);
            return _mapper.Map<LegalDocumentViewModel>(resultModel);
        }

        public async Task<RegistryPageViewModel<LegalDocumentViewModel>> GetRegistryPage(RegistryQuery query)
        {
            var manager = new RegistryPageManager<LegalDocumentModel, LegalDocumentFilterModel>(_legalDocumentService, _mapper);
            var vm = await manager.GetRegistryPage<LegalDocumentViewModel>(query);
            return vm;
        }

        public async Task<bool> CheckPermNameExists(string permName)
        {
            return await _legalDocumentService.CheckPermNameExists(permName);
        }

        public async Task<bool> CheckTranslationExists(string permName, string culture)
        {
            return await _legalDocumentService.CheckTranslationExists(permName, culture);
        }

        public async Task<IEnumerable<string>> GetMissingCultures(string permName) => await _legalDocumentService.GetMissingCultures(permName);

        private async Task FillHasAllTranslations(List<LegalDocumentViewModel> items)
        {
            var permNames = items.Select(m => m.PermName);
            foreach (var permName in permNames)
            {
                var has = await _legalDocumentService.CheckHasAllTranslations(permName);
                foreach (var item in items.Where(m => m.PermName == permName))
                {
                    item.HasAllTranslations = has;
                }
            }
        }
    }
}
