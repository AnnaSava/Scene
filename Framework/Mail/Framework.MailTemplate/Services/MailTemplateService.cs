using AutoMapper;
using Framework.Base.DataService.Contract.Models;
using Framework.Base.Service.ListView;
using Framework.MailTemplate.Data.Contract;
using Framework.MailTemplate.Data.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.MailTemplate.Services
{
    public class MailTemplateService : IMailTemplateService
    {
        private readonly IMailTemplateDbService _mailTemplateDbService;
        private readonly IMapper _mapper;

        public MailTemplateService(IMailTemplateDbService mailTemplateDbService, IMapper mapper)
        {
            _mailTemplateDbService = mailTemplateDbService;
            _mapper = mapper;
        }

        public async Task<TResult> GetOne<TResult>(long id)
        {
            var model = await _mailTemplateDbService.GetOne(id);
            return _mapper.Map<TResult>(model);
        }

        public async Task<TResult> GetActual<TResult>(string permName, string culture)
        {
            var model = await _mailTemplateDbService.GetActual(permName, culture);
            return _mapper.Map<TResult>(model);
        }

        public async Task<MailTemplateViewModel> Create(MailTemplateFormViewModel model)
        {
            var entity = _mapper.Map<MailTemplateModel>(model);
            entity.Culture = entity.Culture.ToLower();
            var created = await _mailTemplateDbService.Create(entity);
            return _mapper.Map<MailTemplateViewModel>(created);
        }

        public async Task<MailTemplateViewModel> CreateTranslation(MailTemplateFormViewModel model)
        {
            var entity = _mapper.Map<MailTemplateModel>(model);
            entity.Culture = entity.Culture.ToLower();
            var created = await _mailTemplateDbService.CreateTranslation(entity);
            return _mapper.Map<MailTemplateViewModel>(created);
        }

        public async Task<MailTemplateViewModel> Update(long id, MailTemplateFormViewModel model)
        {
            var newModel = _mapper.Map<MailTemplateModel>(model);
            newModel.Id = id;
            var resultModel = await _mailTemplateDbService.Update(newModel);
            return _mapper.Map<MailTemplateViewModel>(resultModel);
        }

        public async Task Publish(long id)
        {
            await _mailTemplateDbService.Publish(id);
        }

        public async Task<MailTemplateViewModel> CreateVersion(MailTemplateFormViewModel model)
        {
            var entity = _mapper.Map<MailTemplateModel>(model);
            entity.Culture = entity.Culture.ToLower();
            var created = await _mailTemplateDbService.CreateVersion(entity);
            return _mapper.Map<MailTemplateViewModel>(created);
        }

        public async Task<MailTemplateViewModel> Delete(long id)
        {
            var resultModel = await _mailTemplateDbService.Delete(id);
            return _mapper.Map<MailTemplateViewModel>(resultModel);
        }

        public async Task<MailTemplateViewModel> Restore(long id)
        {
            var resultModel = await _mailTemplateDbService.Restore(id);
            return _mapper.Map<MailTemplateViewModel>(resultModel);
        }

        public async Task<ListPageViewModel<MailTemplateViewModel>> GetAll(MailTemplateFilterViewModel filter, ListPageInfoViewModel pageInfo)
        {
            var filterModel = _mapper.Map<MailTemplateFilterModel>(filter);

            var pageInfoModel = _mapper.Map<PageInfoModel>(pageInfo);

            var list = await _mailTemplateDbService.GetAll(new ListQueryModel<MailTemplateFilterModel> { Filter = filterModel, PageInfo = pageInfoModel });

            var vm = new ListPageViewModel<MailTemplateViewModel>()
            {
                Items = list.Items.Select(m => _mapper.Map<MailTemplateModel, MailTemplateViewModel>(m)),
                Page = list.Page,
                TotalPages = list.TotalPages,
                TotalRows = list.TotalRows
            };

            return vm;
        }

        public async Task<bool> CheckPermNameExists(string permName)
        {
            return await _mailTemplateDbService.CheckPermNameExists(permName);
        }

        public async Task<bool> CheckTranslationExists(string permName, string culture)
        {
            return await _mailTemplateDbService.CheckTranslationExists(permName, culture);
        }

        public async Task<IEnumerable<string>> GetMissingCultures(string permName) => await _mailTemplateDbService.GetMissingCultures(permName);

        private async Task FillHasAllTranslations(List<MailTemplateViewModel> items)
        {
            var permNames = items.Select(m => m.PermName);
            foreach (var permName in permNames)
            {
                var has = await _mailTemplateDbService.CheckHasAllTranslations(permName);
                foreach (var item in items.Where(m => m.PermName == permName))
                {
                    item.HasAllTranslations = has;
                }
            }
        }
    }
}
