using Framework.Base.DataService.Contract.Models;
using Framework.Base.DataService.Contract.Models.ListView;
using Framework.Base.Types.Registry;
using Framework.User.DataService.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.DataService.Contract.Interfaces
{
    [Obsolete]
    public interface IPermissionDbService
    {
        Task<OperationResult<PermissionModel>> Create(PermissionModel model);

        Task<PageListModel<PermissionModel>> GetAll(ListQueryModel<PermissionFilterModel> query);

        Task<IEnumerable<string>> FilterExisting(IEnumerable<string> names);

        Task<Dictionary<string, List<string>>> GetTree();
    }
}
