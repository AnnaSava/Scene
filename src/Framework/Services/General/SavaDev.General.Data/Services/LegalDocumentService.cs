using AutoMapper;
using Microsoft.Extensions.Logging;
using SavaDev.Base.Data.Context;
using SavaDev.Base.Data.Managers;
using SavaDev.Base.Data.Registry;
using SavaDev.Base.Data.Services;
using SavaDev.General.Data.Contract;
using SavaDev.General.Data.Contract.Context;
using SavaDev.General.Data.Contract.Models;
using SavaDev.General.Data.Entities;

namespace SavaDev.General.Data.Services
{
    public class LegalDocumentService : BaseDocumentEntityService<LegalDocument, LegalDocumentModel>, 
        ILegalDocumentService
    {
        protected readonly AllSelector<long, LegalDocument> selectorManager;

        public LegalDocumentService(IGeneralContext dbContext, 
            IEnumerable<string> availableCultures, 
            IMapper mapper, 
            ILogger<LegalDocumentService> logger)
            : base(dbContext, availableCultures, mapper, logger, nameof(LegalDocumentService))
        {
            selectorManager = new AllSelector<long, LegalDocument>(dbContext, mapper, logger);
        }

        public async Task<RegistryPage<LegalDocumentModel>> GetRegistryPage(RegistryQuery<LegalDocumentFilterModel> query)
        {
            var page = await selectorManager.GetRegistryPage<LegalDocumentFilterModel, LegalDocumentModel>(query);
            return page;
        }
    }
}
