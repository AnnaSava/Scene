using Framework.Base.DataService.Contract.Models;
using Framework.Base.DataService.Contract.Models.ListView;
using Framework.User.DataService.Contract.Models;
using System.Threading.Tasks;

namespace Framework.User.DataService.Contract.Interfaces
{
    public interface IConsentDbService
    {
        Task<ConsentModel> Create(ConsentModel model);

        Task<ConsentModel> Update(ConsentModel model);

        Task<ConsentModel> Delete(int id);

        Task<ConsentModel> Restore(int id);

        Task<ConsentModel> GetOne(int id);

        Task<ConsentModel> GetActual();

        Task<PageListModel<ConsentModel>> GetAll(ListQueryModel<ConsentFilterModel> query);

        Task<bool> AnyConsentExists();

        Task<bool> IsActual(int id);

        Task<bool> IsLast(int id);
    }
}
