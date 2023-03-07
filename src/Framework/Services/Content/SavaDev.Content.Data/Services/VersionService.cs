using AutoMapper;
using Microsoft.Extensions.Logging;
using SavaDev.Content.Data;
using SavaDev.Content.Data.Contract;
using SavaDev.Content.Data.Contract.Models;
using SavaDev.Content.Data.Services;
using SavaDev.Base.Data.Managers;
using SavaDev.Base.Data.Registry;

namespace SavaDev.Content.Data.Services
{
    public class VersionService : BaseContentEntityService<Entities.Version, VersionModel, VersionFilterModel>, IVersionService
    {
        public VersionService(ContentContext dbContext, IMapper mapper, ILogger<VersionService> logger)
            : base (dbContext, mapper, logger) 
        {
            AllSelector = new AllSelector<Guid, Entities.Version>(dbContext, mapper, logger);
        }

        public async Task<RegistryPage<VersionModel>> GetRegistryPage(RegistryQuery<VersionFilterModel> query)
        {
            var page = await AllSelector.GetRegistryPage<VersionFilterModel, VersionModel>(query);
            return page;
        }
    }
}
