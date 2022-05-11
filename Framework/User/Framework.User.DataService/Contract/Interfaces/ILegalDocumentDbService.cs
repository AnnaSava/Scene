using Framework.Base.DataService.Contract.Models;
using Framework.Base.DataService.Contract.Models.ListView;
using Framework.User.DataService.Contract.Models;
using System.Threading.Tasks;

namespace Framework.User.DataService.Contract.Interfaces
{
    public interface ILegalDocumentDbService
    {
        Task<LegalDocumentModel> Create(LegalDocumentModel model);

        Task<LegalDocumentModel> Update(LegalDocumentModel model);

        Task<LegalDocumentModel> Delete(long id);

        Task<LegalDocumentModel> Restore(long id);

        Task<LegalDocumentModel> GetOne(long id);

        Task<PageListModel<LegalDocumentModel>> GetAll(ListQueryModel<LegalDocumentFilterModel> query);
    }
}
