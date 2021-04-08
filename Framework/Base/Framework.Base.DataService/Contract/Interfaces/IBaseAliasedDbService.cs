using Framework.Base.DataService.Contract.Models;
using System.Threading.Tasks;

namespace Framework.Base.DataService.Contract.Interfaces
{
    public interface IBaseAliasedDbService<TModel> : IBaseDbService<TModel>
        where TModel : BaseModel<long>
    {
        Task<TModel> GetOneByAlias(string alias);
    }
}
