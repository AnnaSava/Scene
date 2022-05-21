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

        public async Task<bool> CheckDocumentExisis(string permName)
        {
            return await _mailTemplateDbService.CheckDocumentExisis(permName);
        }

        public async Task<bool> CheckTranslationExisis(string permName, string culture)
        {
            return await _mailTemplateDbService.CheckTranslationExisis(permName, culture);
        }
    }
}
