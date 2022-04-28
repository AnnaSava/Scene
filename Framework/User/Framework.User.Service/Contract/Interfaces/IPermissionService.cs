using Framework.Base.Service.ListView;
using Framework.User.Service.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.Service.Contract.Interfaces
{
    public interface IPermissionService
    {
        Task<ListPageViewModel<PermissionViewModel>> GetAll(PermissionFilterViewModel filter, ListPageInfoViewModel pageInfo);

        Task<IEnumerable<PermissionTreeNodeViewModel>> GetTree();
    }
}
