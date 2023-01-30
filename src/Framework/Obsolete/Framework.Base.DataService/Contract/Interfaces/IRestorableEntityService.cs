using Framework.Base.DataService.Contract.Models.ListView;
using Framework.Base.Types.ModelTypes;
using System.Threading.Tasks;

namespace Framework.Base.DataService.Contract.Interfaces
{
    public interface IRestorableEntityService<TModel>
        where TModel : IModelRestorable
    {
        Task<TModel> Create(TModel model);

        Task<TModel> Update(long id, TModel model);

        Task<TModel> Delete(long id);

        Task<TModel> Restore(long id);

        Task<TModel> GetOne(long id);

        Task<PageListModel<TModel>> GetAll(int page, int count);
    }
}
