using Framework.Base.Service.ListView;
using SavaDev.Users.Front.Contract.Models;

namespace SavaDev.Users.Front.Contract
{
    public interface IRoleViewService
    {
        //Task<AppRoleViewModel> GetOne(long id);

        Task<ListPageViewModel<RoleViewModel>> GetAll(RoleFilterViewModel filter, ListPageInfoViewModel pageInfo);

        //Task<AppRoleViewModel> Create(AppRoleFormViewModel model);

        //Task<AppRoleViewModel> Update(long id, AppRoleFormViewModel model);

        //Task<AppRoleViewModel> Delete(long id);

        //Task<AppRoleViewModel> Restore(long id);
    }
}
