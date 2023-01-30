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
    public interface IAppRoleDbService : IRoleDbService<AppRoleModel>
    {
        Task<PageListModel<AppRoleModel>> GetAll(ListQueryModel<AppRoleFilterModel> query);
    }
}
