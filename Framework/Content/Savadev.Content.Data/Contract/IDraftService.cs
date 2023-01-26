using Framework.Base.DataService.Contract.Models;
using Framework.Base.DataService.Contract.Models.ListView;
using Framework.Base.Types.View;
using Savadev.Content.Data.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savadev.Content.Data.Contract
{
    public interface IDraftService
    {
        Task<OperationResult<DraftModel>> Create<T>(DraftModel model, T contentModel);

        Task<OperationResult> Update<T>(Guid id, T contentModel);

        Task<OperationResult> SetContentId(Guid id, string contentId);

        Task<OperationResult> Delete(Guid id);

        Task<DraftModel> GetOne(Guid id);

        Task<PageListModel<DraftModel>> GetAll(ListQueryModel<DraftStrictFilterModel> query);

        Task<PageListModel<DraftModel>> GetAll(ListQueryModel<DraftFilterModel> query);
    }
}
