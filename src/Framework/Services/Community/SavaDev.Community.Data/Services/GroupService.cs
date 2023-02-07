using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SavaDev.Base.Data.Managers;
using SavaDev.Base.Data.Registry;
using SavaDev.Base.Data.Services;
using SavaDev.Community.Data.Contract.Models;
using SavaDev.Community.Data.Entities;
using SavaDev.Community.Service.Contract;
using X.PagedList;

namespace SavaDev.Community.Data.Services
{
    public class GroupService : BaseHubEntityService<Guid, Group, GroupModel>, IGroupService
    {
        public GroupService(CommunityContext dbContext, IMapper mapper, ILogger<GroupService> logger) 
            : base(dbContext, mapper, logger) 
        {
            AllSelector = new AllSelector<Guid, Group>(GetInftrastructure);
        }

        public async Task<RegistryPage<RoleModel>> GetRegistryPage(RegistryQuery<GroupFilterModel> query)
        {
            var page = await AllSelector.GetRegistryPage<GroupFilterModel, RoleModel>(query);
            return page;
        }
    }
}
