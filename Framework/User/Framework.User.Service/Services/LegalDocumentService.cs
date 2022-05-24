using AutoMapper;
using Framework.Base.DataService.Contract.Models;
using Framework.Base.Service.ListView;
using Framework.User.DataService.Contract.Interfaces;
using Framework.User.DataService.Contract.Models;
using Framework.User.Service.Contract.Interfaces;
using Framework.User.Service.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.Service.Services
{
    public class LegalDocumentService : ILegalDocumentService
    {
        private readonly ILegalDocumentDbService _legalDocumentDbService;
        private readonly IMapper _mapper;

        public LegalDocumentService(ILegalDocumentDbService legalDocumentDbService, IMapper mapper)
        {
            _legalDocumentDbService = legalDocumentDbService;
            _mapper = mapper;
        }

        public async Task<TResult> GetOne<TResult>(long id)
        {
            var model = await _legalDocumentDbService.GetOne(id);
            return _mapper.Map<TResult>(model);
        }

        public async Task<TResult> GetActual<TResult>(string permName, string culture)
        {
            var model = await _legalDocumentDbService.GetActual(permName, culture);
            return _mapper.Map<TResult>(model);
        }

        public async Task<LegalDocumentViewModel> Create(LegalDocumentFormViewModel model)
        {
            var entity = _mapper.Map<LegalDocumentModel>(model);
            entity.Culture = entity.Culture.ToLower();
            var created = await _legalDocumentDbService.Create(entity);
            return _mapper.Map<LegalDocumentViewModel>(created);
        }

        public async Task<LegalDocumentViewModel> CreateTranslation(LegalDocumentFormViewModel model)
        {
            var entity = _mapper.Map<LegalDocumentModel>(model);
            entity.Culture = entity.Culture.ToLower();
            var created = await _legalDocumentDbService.CreateTranslation(entity);
            return _mapper.Map<LegalDocumentViewModel>(created);
        }

        public async Task<LegalDocumentViewModel> Update(long id, LegalDocumentFormViewModel model)
        {
            var newModel = _mapper.Map<LegalDocumentModel>(model);
            newModel.Id = id;
            var resultModel = await _legalDocumentDbService.Update(newModel);
            return _mapper.Map<LegalDocumentViewModel>(resultModel);
        }

        public async Task Publish(long id)
        {
            await _legalDocumentDbService.Publish(id);
        }

        public async Task<LegalDocumentViewModel> CreateVersion(LegalDocumentFormViewModel model)
        {
            var entity = _mapper.Map<LegalDocumentModel>(model);
            entity.Culture = entity.Culture.ToLower();
            var created = await _legalDocumentDbService.CreateVersion(entity);
            return _mapper.Map<LegalDocumentViewModel>(created);
        }

        public async Task<LegalDocumentViewModel> Delete(int id)
        {
            var resultModel = await _legalDocumentDbService.Delete(id);
            return _mapper.Map<LegalDocumentViewModel>(resultModel);
        }

        public async Task<LegalDocumentViewModel> Restore(int id)
        {
            var resultModel = await _legalDocumentDbService.Restore(id);
            return _mapper.Map<LegalDocumentViewModel>(resultModel);
        }

        public async Task<ListPageViewModel<LegalDocumentViewModel>> GetAll(LegalDocumentFilterViewModel filter, ListPageInfoViewModel pageInfo)
        {
            var filterModel = _mapper.Map<LegalDocumentFilterModel>(filter);

            var pageInfoModel = _mapper.Map<PageInfoModel>(pageInfo);

            var list = await _legalDocumentDbService.GetAll(new ListQueryModel<LegalDocumentFilterModel> { Filter = filterModel, PageInfo = pageInfoModel });

            var items = list.Items.Select(m => _mapper.Map<LegalDocumentModel, LegalDocumentViewModel>(m)).ToList();

            await FillHasAllTranslations(items);

            var vm = new ListPageViewModel<LegalDocumentViewModel>()
            {
                Items = items,
                Page = list.Page,
                TotalPages = list.TotalPages,
                TotalRows = list.TotalRows
            };

            return vm;
        }

        public async Task<bool> CheckDocumentExisis(string permName)
        {
            return await _legalDocumentDbService.CheckDocumentExisis(permName);
        }

        public async Task<bool> CheckTranslationExisis(string permName, string culture)
        {
            return await _legalDocumentDbService.CheckTranslationExisis(permName, culture);
        }

        public async Task<IEnumerable<string>> GetMissingCultures(string permName) => await _legalDocumentDbService.GetMissingCultures(permName);

        private async Task FillHasAllTranslations(List<LegalDocumentViewModel> items)
        {
            var permNames = items.Select(m => m.PermName);
            foreach (var permName in permNames)
            {
                var has = await _legalDocumentDbService.CheckHasAllTranslations(permName);
                foreach (var item in items.Where(m => m.PermName == permName))
                {
                    item.HasAllTranslations = has;
                }
            }
        }
    }
}
