using Framework.Base.DataService.Contract.Models;
using Framework.Base.DataService.Contract.Models.ListView;
using Framework.User.DataService.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.DataService.Contract.Interfaces
{
    public interface IFrameworkRoleDbService : IRoleDbService<FrameworkRoleModel>
    {
        Task<PageListModel<FrameworkRoleModel>> GetAll(ListQueryModel<FrameworkRoleFilterModel> query);
    }
}
