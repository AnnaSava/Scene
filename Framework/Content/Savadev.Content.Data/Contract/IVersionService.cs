using Framework.Base.DataService;
using Framework.Base.DataService.Contract.Models;
using Framework.Base.DataService.Contract.Models.ListView;
using Framework.Base.Service;
using Framework.Base.Types.View;
using Savadev.Content.Data.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savadev.Content.Data.Contract
{
    public interface IVersionService
    {
        Task<OperationResult<VersionModel>> Create<T>(VersionModel model, T contentModel);

        Task<PageListModel<VersionModel>> GetAll(ListQueryModel<VersionFilterModel> query);
    }
}
