using AutoMapper;
using Framework.Base.DataService.Contract.Models;
using Framework.Base.Service.ListView;
using Framework.User.DataService.Contract.Interfaces;
using Framework.User.DataService.Contract.Models;
using Framework.User.Service.Contract.Interfaces;
using Framework.User.Service.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.Service.Services
{
    public class FrameworkRoleService : BaseRoleService, IFrameworkRoleService
    {
        private readonly IFrameworkRoleDbService _roleDbService;
        private readonly IPermissionDbService _permissionDbService;
        private readonly IMapper _mapper;

        public FrameworkRoleService(IFrameworkRoleDbService roleDbService, IPermissionDbService permissionDbService, IMapper mapper)
        {
            _roleDbService = roleDbService;
            _permissionDbService = permissionDbService;
            _mapper = mapper;
        }

        public async Task<FrameworkRoleViewModel> GetOne(long id)
        {
            var model = await _roleDbService.GetOne(id);
            return _mapper.Map<FrameworkRoleViewModel>(model);
        }

        public async Task<FrameworkRoleViewModel> Create(FrameworkRoleFormViewModel model)
        {
            model.Permissions = await _permissionDbService.FilterExisting(model.Permissions);

            var newModel = _mapper.Map<FrameworkRoleModel>(model);
            var resultModel = await _roleDbService.Create(newModel);
            return _mapper.Map<FrameworkRoleViewModel>(resultModel);
        }

        public async Task<FrameworkRoleViewModel> Update(long id, FrameworkRoleFormViewModel model)
        {
            model.Permissions = await _permissionDbService.FilterExisting(model.Permissions);

            var newModel = _mapper.Map<FrameworkRoleModel>(model);
            newModel.Id = id;
            var resultModel = await _roleDbService.Update(newModel);
            return _mapper.Map<FrameworkRoleViewModel>(resultModel);
        }

        public async Task<FrameworkRoleViewModel> Delete(long id)
        {
            var resultModel = await _roleDbService.Delete(id);
            return _mapper.Map<FrameworkRoleViewModel>(resultModel);
        }

        public async Task<FrameworkRoleViewModel> Restore(long id)
        {
            var resultModel = await _roleDbService.Restore(id);
            return _mapper.Map<FrameworkRoleViewModel>(resultModel);
        }

        public async Task<ListPageViewModel<FrameworkRoleViewModel>> GetAll(FrameworkRoleFilterViewModel filter, ListPageInfoViewModel pageInfo)
        {
            var filterModel = _mapper.Map<FrameworkRoleFilterModel>(filter);

            var pageInfoModel = _mapper.Map<PageInfoModel>(pageInfo);

            var list = await _roleDbService.GetAll(new ListQueryModel<FrameworkRoleFilterModel> { Filter = filterModel, PageInfo = pageInfoModel });

            var vm = new ListPageViewModel<FrameworkRoleViewModel>()
            {
                Items = list.Items.Select(m => _mapper.Map<FrameworkRoleModel, FrameworkRoleViewModel>(m)),
                Page = list.Page,
                TotalPages = list.TotalPages,
                TotalRows = list.TotalRows
            };

            return vm;
        }
    }
}
