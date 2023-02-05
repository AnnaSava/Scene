using SavaDev.Base.Data.Models.Interfaces;
using System.Threading.Tasks;

namespace SavaDev.Base.User.Data.Services.Interfaces
{
    public interface IRoleDbService<TModel> where TModel : IModel<long>
    {
        Task<TModel> GetOne(long id);

        Task<TModel> Create(TModel model);

        Task<TModel> Update(TModel model);

        Task<TModel> Delete(long id);

        Task<TModel> Restore(long id);
    }
}
