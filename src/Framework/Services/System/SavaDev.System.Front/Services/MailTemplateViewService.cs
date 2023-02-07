using AutoMapper;
using SavaDev.Base.Data.Registry;
using SavaDev.Base.Front.Registry;
using SavaDev.Base.Front.Services;
using SavaDev.System.Data.Contract;
using SavaDev.System.Data.Contract.Models;
using SavaDev.System.Front.Contract;
using SavaDev.System.Front.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.System.Front.Services
{
    public class MailTemplateViewService : IMailTemplateViewService
    {
        private readonly IMailTemplateService _mailTemplateService;
        private readonly IMapper _mapper;

        public MailTemplateViewService(IMailTemplateService mailTemplateDbService, IMapper mapper)
        {
            _mailTemplateService = mailTemplateDbService;
            _mapper = mapper;
        }

        public async Task<TResult> GetOne<TResult>(long id)
        {
            var model = await _mailTemplateService.GetOne<MailTemplateModel>(id);
            return _mapper.Map<TResult>(model);
        }

        public async Task<TResult> GetActual<TResult>(string permName, string culture)
        {
            var model = await _mailTemplateService.GetActual<MailTemplateModel>(permName, culture);
            return _mapper.Map<TResult>(model);
        }

        public async Task<MailTemplateViewModel> Create(MailTemplateFormViewModel model)
        {
            var entity = _mapper.Map<MailTemplateModel>(model);
            entity.Culture = entity.Culture.ToLower();
            var created = await _mailTemplateService.Create(entity);
            return _mapper.Map<MailTemplateViewModel>(created);
        }

        public async Task<MailTemplateViewModel> CreateTranslation(MailTemplateFormViewModel model)
        {
            var entity = _mapper.Map<MailTemplateModel>(model);
            entity.Culture = entity.Culture.ToLower();
            var created = await _mailTemplateService.CreateTranslation(entity);
            return _mapper.Map<MailTemplateViewModel>(created);
        }

        public async Task<MailTemplateViewModel> Update(long id, MailTemplateFormViewModel model)
        {
            var newModel = _mapper.Map<MailTemplateModel>(model);
            newModel.Id = id;
            var resultModel = await _mailTemplateService.Update(id, newModel);
            return _mapper.Map<MailTemplateViewModel>(resultModel);
        }

        public async Task Publish(long id)
        {
            await _mailTemplateService.Publish(id);
        }

        public async Task<MailTemplateViewModel> CreateVersion(MailTemplateFormViewModel model)
        {
            var entity = _mapper.Map<MailTemplateModel>(model);
            entity.Culture = entity.Culture.ToLower();
            var created = await _mailTemplateService.CreateVersion(entity);
            return _mapper.Map<MailTemplateViewModel>(created);
        }

        public async Task<MailTemplateViewModel> Delete(long id)
        {
            var resultModel = await _mailTemplateService.Delete(id);
            return _mapper.Map<MailTemplateViewModel>(resultModel);
        }

        public async Task<MailTemplateViewModel> Restore(long id)
        {
            var resultModel = await _mailTemplateService.Restore(id);
            return _mapper.Map<MailTemplateViewModel>(resultModel);
        }

        public async Task<bool> CheckPermNameExists(string permName)
        {
            return await _mailTemplateService.CheckPermNameExists(permName);
        }

        public async Task<bool> CheckTranslationExists(string permName, string culture)
        {
            return await _mailTemplateService.CheckTranslationExists(permName, culture);
        }

        public async Task<IEnumerable<string>> GetMissingCultures(string permName) => await _mailTemplateService.GetMissingCultures(permName);

        private async Task FillHasAllTranslations(List<MailTemplateViewModel> items)
        {
            var permNames = items.Select(m => m.PermName);
            foreach (var permName in permNames)
            {
                var has = await _mailTemplateService.CheckHasAllTranslations(permName);
                foreach (var item in items.Where(m => m.PermName == permName))
                {
                    item.HasAllTranslations = has;
                }
            }
        }

        public async Task<RegistryPageViewModel<MailTemplateViewModel>> GetRegistryPage(RegistryQuery query)
        {
            var manager = new RegistryPageManager<MailTemplateModel, MailTemplateFilterModel>(_mailTemplateService, _mapper);
            var vm = await manager.GetRegistryPage<MailTemplateViewModel>(query);
            return vm;
        }
    }
}
