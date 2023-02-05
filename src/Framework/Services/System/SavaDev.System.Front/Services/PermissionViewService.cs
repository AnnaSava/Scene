using AutoMapper;
using SavaDev.System.Data.Contract;
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
        private readonly IPermissionService _permissionDbService;
        private readonly IMapper _mapper;

        public PermissionViewService(IPermissionService permissionDbService, IMapper mapper)
        {
            _permissionDbService = permissionDbService;
            _mapper = mapper;
        }

        //public async Task<ListPageViewModel<PermissionViewModel>> GetAll(PermissionFilterViewModel filter, ListPageInfoViewModel pageInfo)
        //{
        //    var filterModel = _mapper.Map<PermissionFilterModel>(filter);

        //    var pageInfoModel = _mapper.Map<PageInfoModel>(pageInfo);

        //    var list = await _permissionDbService.GetAll(new ListQueryModel<PermissionFilterModel> { Filter = filterModel, PageInfo = pageInfoModel });

        //    var vm = ListPageViewModel.Map<PermissionModel, PermissionViewModel>(list, _mapper);
        //    return vm;
        //}

        public async Task<IEnumerable<PermissionTreeNodeViewModel>> GetTree()
        {
            var dict = await _permissionDbService.GetTree();

            return dict.Select(m => new PermissionTreeNodeViewModel { Group = m.Key, Permissions = m.Value.Select(p => new PermissionViewModel { Name = p }).ToList() });
        }
    }
}
