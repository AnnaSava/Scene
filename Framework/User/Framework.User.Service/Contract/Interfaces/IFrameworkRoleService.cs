using Framework.Base.Service.ListView;
using Framework.User.Service.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.Service.Contract.Interfaces
{
    public interface IFrameworkRoleService
    {
        Task<FrameworkRoleViewModel> GetOne(long id);

        Task<ListPageViewModel<FrameworkRoleViewModel>> GetAll(FrameworkRoleFilterViewModel filter, ListPageInfoViewModel pageInfo);

        Task<FrameworkRoleViewModel> Create(FrameworkRoleFormViewModel model);

        Task<FrameworkRoleViewModel> Update(long id, FrameworkRoleFormViewModel model);

        Task<FrameworkRoleViewModel> Delete(long id);

        Task<FrameworkRoleViewModel> Restore(long id);
    }
}
