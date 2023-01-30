using Framework.Base.DataService.Contract.Models;
using Framework.Base.DataService.Contract.Models.ListView;
using Framework.Base.Types.Registry;
using Savadev.Content.Data.Contract.Models;

namespace Savadev.Content.Data.Contract
{
    public interface IVersionService
    {
        Task<OperationResult<VersionModel>> Create<T>(VersionModel model, T contentModel);

        Task<PageListModel<VersionModel>> GetAll(ListQueryModel<VersionFilterModel> query);
    }
}
