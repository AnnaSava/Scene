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
    public class AppRoleService : BaseRoleService, IAppRoleService
    {
        private readonly IAppRoleDbService _roleDbService;
        private readonly IPermissionDbService _permissionDbService;
        private readonly IMapper _mapper;

        public AppRoleService(IAppRoleDbService roleDbService, IPermissionDbService permissionDbService, IMapper mapper)
        {
            _roleDbService = roleDbService;
            _permissionDbService = permissionDbService;
            _mapper = mapper;
        }

        public async Task<AppRoleViewModel> GetOne(long id)
        {
            var model = await _roleDbService.GetOne(id);
            return _mapper.Map<AppRoleViewModel>(model);
        }

        public async Task<AppRoleViewModel> Create(AppRoleFormViewModel model)
        {
            model.Permissions = await _permissionDbService.FilterExisting(model.Permissions);

            var newModel = _mapper.Map<AppRoleModel>(model);
            var resultModel = await _roleDbService.Create(newModel);
            return _mapper.Map<AppRoleViewModel>(resultModel);
        }

        public async Task<AppRoleViewModel> Update(long id, AppRoleFormViewModel model)
        {
            model.Permissions = await _permissionDbService.FilterExisting(model.Permissions);

            var newModel = _mapper.Map<AppRoleModel>(model);
            newModel.Id = id;
            var resultModel = await _roleDbService.Update(newModel);
            return _mapper.Map<AppRoleViewModel>(resultModel);
        }

        public async Task<AppRoleViewModel> Delete(long id)
        {
            var resultModel = await _roleDbService.Delete(id);
            return _mapper.Map<AppRoleViewModel>(resultModel);
        }

        public async Task<AppRoleViewModel> Restore(long id)
        {
            var resultModel = await _roleDbService.Restore(id);
            return _mapper.Map<AppRoleViewModel>(resultModel);
        }

        public async Task<ListPageViewModel<AppRoleViewModel>> GetAll(AppRoleFilterViewModel filter, ListPageInfoViewModel pageInfo)
        {
            var filterModel = _mapper.Map<AppRoleFilterModel>(filter);

            var pageInfoModel = _mapper.Map<PageInfoModel>(pageInfo);

            var list = await _roleDbService.GetAll(new ListQueryModel<AppRoleFilterModel> { Filter = filterModel, PageInfo = pageInfoModel });

            var vm = ListPageViewModel.Map<AppRoleModel, AppRoleViewModel>(list, _mapper);

            return vm;
        }
    }
}
