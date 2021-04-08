using Framework.Base.DataService.Contract.Models;
using System.Threading.Tasks;
using X.PagedList;

namespace Framework.Base.DataService.Contract.Interfaces
{
    public interface IBaseDbService<TModel>
        where TModel : BaseModel<long>
    {
        Task<TModel> Create(TModel model);

        Task<TModel> Update(TModel model);

        Task<TModel> Delete(long id);

        Task<TModel> Restore(long id);

        Task<TModel> GetOne(long id);

        Task<IPagedList<TModel>> GetAll(int pageNumber, int pageSize);
    }
}
