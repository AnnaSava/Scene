using AutoMapper;
using SavaDev.Base.Data.Registry;
using SavaDev.Base.Front.Registry;
using SavaDev.Base.Front.Services;
using SavaDev.System.Data.Contract;
using SavaDev.System.Data.Contract.Models;
using SavaDev.System.Data.Services;
using SavaDev.System.Front.Contract;
using SavaDev.System.Front.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.System.Front.Services
{
    public class PermissionViewService : IPermissionViewService
    {
        private readonly IPermissionService _permissionService;
        private readonly IMapper _mapper;

        public PermissionViewService(IPermissionService permissionDbService, IMapper mapper)
        {
            _permissionService = permissionDbService;
            _mapper = mapper;
        }

        public async Task<RegistryPageViewModel<PermissionViewModel>> GetRegistryPage(RegistryQuery query)
        {
            var manager = new RegistryPageManager<PermissionModel, PermissionFilterModel>(_permissionService, _mapper);
            var vm = await manager.GetRegistryPage<PermissionViewModel>(query);
            return vm;
        }

        public async Task<IEnumerable<PermissionTreeNodeViewModel>> GetTree()
        {
            var dict = await _permissionService.GetTree();

            return dict.Select(m => new PermissionTreeNodeViewModel { Group = m.Key, Permissions = m.Value.Select(p => new PermissionViewModel { Name = p }).ToList() });
        }
    }
}
