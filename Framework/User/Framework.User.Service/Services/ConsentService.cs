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
    public class ConsentService : IConsentService
    {
        private readonly IConsentDbService _consentDbService;
        private readonly IMapper _mapper;

        public ConsentService(IConsentDbService consentNameDbService, IMapper mapper)
        {
            _consentDbService = consentNameDbService;
            _mapper = mapper;
        }

        public async Task<ConsentViewModel> GetOne(int id)
        {
            var model = await _consentDbService.GetOne(id);
            return _mapper.Map<ConsentViewModel>(model);
        }

        public async Task<ConsentViewModel> Create(ConsentViewModel model)
        {
            if (await _consentDbService.AnyConsentExists())
                throw new Exception("Consent already exists.");

            var entity = _mapper.Map<ConsentModel>(model);
            var created = await _consentDbService.Create(entity);
            return _mapper.Map<ConsentViewModel>(created);
        }

        public async Task<ConsentViewModel> Update(ConsentViewModel model)
        {
            var newModel = _mapper.Map<ConsentModel>(model);

            var currentModel = await _consentDbService.GetOne(model.Id);
            ConsentModel resultModel;

            if (await _consentDbService.IsLast(model.Id) || await _consentDbService.IsActual(model.Id))
            {
                if (currentModel.IsApproved)
                {
                    resultModel = await _consentDbService.Create(newModel);
                }
                else
                {
                    resultModel = await _consentDbService.Update(newModel);
                }
            }
            else
            {
                throw new Exception("Cannot update archived consent.");
            }
            
            return _mapper.Map<ConsentViewModel>(resultModel);
        }

        public async Task<ConsentViewModel> Delete(int id)
        {
            var resultModel = await _consentDbService.Delete(id);
            return _mapper.Map<ConsentViewModel>(resultModel);
        }

        public async Task<ConsentViewModel> Restore(int id)
        {
            var resultModel = await _consentDbService.Restore(id);
            return _mapper.Map<ConsentViewModel>(resultModel);
        }

        public async Task<ListPageViewModel<ConsentViewModel>> GetAll(ConsentFilterViewModel filter, ListPageInfoViewModel pageInfo)
        {
            var filterModel = _mapper.Map<ConsentFilterModel>(filter);

            var pageInfoModel = _mapper.Map<PageInfoModel>(pageInfo);

            var list = await _consentDbService.GetAll(new ListQueryModel<ConsentFilterModel> { Filter = filterModel, PageInfo = pageInfoModel });

            var vm = new ListPageViewModel<ConsentViewModel>()
            {
                Items = list.Items.Select(m => _mapper.Map<ConsentModel, ConsentViewModel>(m)),
                Page = list.Page,
                TotalPages = list.TotalPages,
                TotalRows = list.TotalRows
            };

            return vm;
        }
    }
}
