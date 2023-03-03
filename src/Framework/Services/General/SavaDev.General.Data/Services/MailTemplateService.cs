using AutoMapper;
using Microsoft.Extensions.Logging;
using SavaDev.Base.Data.Managers;
using SavaDev.Base.Data.Registry;
using SavaDev.Base.Data.Services;
using SavaDev.General.Data.Contract;
using SavaDev.General.Data.Contract.Context;
using SavaDev.General.Data.Contract.Models;
using SavaDev.General.Data.Entities;

namespace SavaDev.General.Data.Services
{
    public class MailTemplateService : BaseDocumentEntityService<MailTemplate, MailTemplateModel>, 
        IMailTemplateService
    {
        protected readonly AllSelector<long, MailTemplate> selectorManager;

        public MailTemplateService(GeneralContext dbContext, IEnumerable<string> availableCultures, IMapper mapper, ILogger<MailTemplateService> logger)
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
