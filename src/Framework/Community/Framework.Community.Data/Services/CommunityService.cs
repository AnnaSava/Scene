using AutoMapper;
using AutoMapper.QueryableExtensions;
using Framework.Base.DataService.Contract.Models.ListView;
using Framework.Base.DataService.Services.Managers;
using Framework.Base.Types.Registry;
using Framework.Community.Data;
using Framework.Community.Data.Contract.Models;
using Framework.Community.Service.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Framework.Community.Data.Services
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

        public async Task<PageListModel<CommunityModel>> GetAll(int page, int count)
        {
            var dbSet = _dbContext.Set<Entities.Community>().AsNoTracking();

            var res = await dbSet.ProjectTo<CommunityModel>(_mapper.ConfigurationProvider).ToPagedListAsync(page, count);

            var pageModel = new PageListModel<CommunityModel>()
            {
                Items = res,
                Page = res.PageNumber,
                TotalPages = res.PageCount,
                TotalRows = res.TotalItemCount
            };

            return pageModel;
        }
    }
}
