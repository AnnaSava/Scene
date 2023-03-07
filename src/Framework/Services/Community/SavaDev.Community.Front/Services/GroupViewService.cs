using AutoMapper;
using SavaDev.Base.Data.Registry;
using SavaDev.Base.Front.Registry;
using SavaDev.Base.Front.Services;
using SavaDev.Community.Data.Contract.Models;
using SavaDev.Community.Front.Contract;
using SavaDev.Community.Front.Contract.Models;
using SavaDev.Community.Service.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Community.Front.Services
{
    public class GroupViewService : IGroupViewService
    {
        private readonly IGroupService _groupService;
        private readonly IMapper _mapper;

        public GroupViewService(IGroupService galleryService,
            IMapper mapper)
        {
            _groupService = galleryService;
            _mapper = mapper;
        }

        public async Task<RegistryPageViewModel<GroupViewModel>> GetRegistryPage(RegistryQuery query)
        {
            var manager = new RegistryPageManager<GroupModel, GroupFilterModel>(_groupService, _mapper);
            var vm = await manager.GetRegistryPage<GroupViewModel>(query);
            return vm;
        }
    }
}
