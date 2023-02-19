using AutoMapper;
using Framework.Base.Service.ListView;
using SavaDev.System.Data.Contract;
using SavaDev.Users.Data;
using SavaDev.Users.Front.Contract;
using SavaDev.Users.Front.Contract.Models;

namespace Framework.User.Service.Services
{
    public class RoleViewService : IRoleViewService
    {
        private readonly IRoleService _roleDbService;
        private readonly IPermissionService _permissionDbService;
        private readonly IMapper _mapper;

        public RoleViewService(IRoleService roleDbService, IPermissionService permissionDbService, IMapper mapper)
        {
            _roleDbService = roleDbService;
            _permissionDbService = permissionDbService;
            _mapper = mapper;
        }

        //public async Task<AppRoleViewModel> GetOne(long id)
        //{
        //    var model = await _roleDbService.GetOne(id);
        //    return _mapper.Map<AppRoleViewModel>(model);
        //}

        //public async Task<AppRoleViewModel> Create(AppRoleFormViewModel model)
        //{
        //    model.Permissions = await _permissionDbService.FilterExisting(model.Permissions);

        //    var newModel = _mapper.Map<AppRoleModel>(model);
        //    var resultModel = await _roleDbService.Create(newModel);
        //    return _mapper.Map<AppRoleViewModel>(resultModel);
        //}

        //public async Task<AppRoleViewModel> Update(long id, AppRoleFormViewModel model)
        //{
        //    model.Permissions = await _permissionDbService.FilterExisting(model.Permissions);

        //    var newModel = _mapper.Map<AppRoleModel>(model);
        //    newModel.Id = id;
        //    var resultModel = await _roleDbService.Update(newModel);
        //    return _mapper.Map<AppRoleViewModel>(resultModel);
        //}

        //public async Task<AppRoleViewModel> Delete(long id)
        //{
        //    var resultModel = await _roleDbService.Delete(id);
        //    return _mapper.Map<AppRoleViewModel>(resultModel);
        //}

        //public async Task<AppRoleViewModel> Restore(long id)
        //{
        //    var resultModel = await _roleDbService.Restore(id);
        //    return _mapper.Map<AppRoleViewModel>(resultModel);
        //}

        public async Task<ListPageViewModel<RoleViewModel>> GetAll(RoleFilterViewModel filter, ListPageInfoViewModel pageInfo)
        {
            throw new NotImplementedException();

            //var filterModel = _mapper.Map<AppRoleFilterModel>(filter);

            //var pageInfoModel = _mapper.Map<PageInfoModel>(pageInfo);

            //var list = await _roleDbService.GetAll(new ListQueryModel<AppRoleFilterModel> { Filter = filterModel, PageInfo = pageInfoModel });

            //var vm = ListPageViewModel.Map<AppRoleModel, AppRoleViewModel>(list, _mapper);

            //return vm;
        }
    }
}
