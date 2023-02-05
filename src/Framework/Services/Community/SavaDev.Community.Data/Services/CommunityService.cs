using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SavaDev.Base.Data.Managers;
using SavaDev.Base.Data.Registry;
using SavaDev.Base.Data.Services;
using SavaDev.Community.Data.Contract.Models;
using SavaDev.Community.Service.Contract;
using X.PagedList;

namespace SavaDev.Community.Data.Services
{
    public class CommunityService : ICommunityService
    {
        private readonly IMapper _mapper;
        private readonly CommunityContext _dbContext;

        private readonly HubEntityManager<Guid, Entities.Community, CommunityModel> entityManager;

        public CommunityService(CommunityContext dbContext, IMapper mapper, ILogger<CommunityService> logger) 
        {
            _dbContext = dbContext;
            _mapper = mapper;

            entityManager = new HubEntityManager<Guid, Entities.Community, CommunityModel>(dbContext, mapper, logger);
        }

        public async Task<OperationResult<CommunityModel>> Create(CommunityModel model) => await entityManager.Create(model);

        public async Task<CommunityModel> GetOne(Guid id) => await entityManager.GetOne<CommunityModel>(id);

        public async Task<ItemsPage<CommunityModel>> GetAll(int page, int count)
        {
            var dbSet = _dbContext.Set<Entities.Community>().AsNoTracking();

            var res = await dbSet.ProjectTo<CommunityModel>(_mapper.ConfigurationProvider).ToPagedListAsync(page, count);

            var pageModel = new ItemsPage<CommunityModel>(res);

            return pageModel;
        }
    }
}
