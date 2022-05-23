using Framework.Base.DataService.Contract.Models;
using Framework.Base.DataService.Contract.Models.ListView;
using Framework.User.DataService.Contract.Models;
using System.Threading.Tasks;

namespace Framework.User.DataService.Contract.Interfaces
{
    public interface ILegalDocumentDbService
    {
        Task<LegalDocumentModel> Create(LegalDocumentModel model);

        Task<LegalDocumentModel> CreateTranslation(LegalDocumentModel model);

        Task<LegalDocumentModel> Update(LegalDocumentModel model);

        Task Publish(long id);

        Task<LegalDocumentModel> CreateVersion(LegalDocumentModel model);

        Task<LegalDocumentModel> Delete(long id);

        Task<LegalDocumentModel> Restore(long id);

        Task<LegalDocumentModel> GetOne(long id);

        Task<LegalDocumentModel> GetActual(string permName, string culture);

        Task<PageListModel<LegalDocumentModel>> GetAll(ListQueryModel<LegalDocumentFilterModel> query);

        Task<bool> CheckDocumentExisis(string permName);

        Task<bool> CheckTranslationExisis(string permName, string culture);

        Task<bool> CheckHasAllTranslations(string permName);
    }
}
