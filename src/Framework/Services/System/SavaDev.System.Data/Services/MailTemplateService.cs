using AutoMapper;
using Microsoft.Extensions.Logging;
using SavaDev.Base.Data.Managers;
using SavaDev.Base.Data.Registry;
using SavaDev.Base.Data.Services;
using SavaDev.System.Data.Contract;
using SavaDev.System.Data.Contract.Context;
using SavaDev.System.Data.Contract.Models;
using SavaDev.System.Data.Entities;

namespace SavaDev.System.Data.Services
{
    public class MailTemplateService : BaseDocumentEntityService<MailTemplate, MailTemplateModel>, 
        IMailTemplateService
    {
        protected readonly AllSelector<long, MailTemplate> selectorManager;

        public MailTemplateService(SystemContext dbContext, IEnumerable<string> availableCultures, IMapper mapper, ILogger<MailTemplateService> logger)
            : base(dbContext, availableCultures, mapper, logger, nameof(MailTemplateService))
        {
            selectorManager = new AllSelector<long, MailTemplate>(dbContext, mapper, logger);
        }

        public async Task<RegistryPage<MailTemplateModel>> GetRegistryPage(RegistryQuery<MailTemplateFilterModel> query)
        {
            var page = await selectorManager.GetRegistryPage<MailTemplateFilterModel, MailTemplateModel>(query);
            return page;
        }
    }
}
