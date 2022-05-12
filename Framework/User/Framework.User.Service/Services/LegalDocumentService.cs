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

        public LegalDocumentService(ILegalDocumentDbService legalDocumentNameDbService, IMapper mapper)
        {
            _legalDocumentDbService = legalDocumentNameDbService;
            _mapper = mapper;
        }

        public async Task<LegalDocumentViewModel> GetOne(int id)
        {
            var model = await _legalDocumentDbService.GetOne(id);
            return _mapper.Map<LegalDocumentViewModel>(model);
        }

        public async Task<LegalDocumentViewModel> Create(LegalDocumentViewModel model)
        {
            var entity = _mapper.Map<LegalDocumentModel>(model);
            var created = await _legalDocumentDbService.Create(entity);
            return _mapper.Map<LegalDocumentViewModel>(created);
        }

        public async Task<LegalDocumentViewModel> CreateTranslation(LegalDocumentViewModel model)
        {
            var entity = _mapper.Map<LegalDocumentModel>(model);
            var created = await _legalDocumentDbService.CreateTranslation(entity);
            return _mapper.Map<LegalDocumentViewModel>(created);
        }

        public async Task<LegalDocumentViewModel> Update(LegalDocumentViewModel model)
        {
            var newModel = _mapper.Map<LegalDocumentModel>(model);
            var resultModel = await _legalDocumentDbService.Update(newModel);
            return _mapper.Map<LegalDocumentViewModel>(resultModel);
        }

        public async Task Approve(long id)
        {
            await _legalDocumentDbService.Approve(id);
        }

        public async Task<LegalDocumentViewModel> CreateVersion(LegalDocumentViewModel model)
        {
            var entity = _mapper.Map<LegalDocumentModel>(model);
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

            var vm = new ListPageViewModel<LegalDocumentViewModel>()
            {
                Items = list.Items.Select(m => _mapper.Map<LegalDocumentModel, LegalDocumentViewModel>(m)),
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
    }
}
