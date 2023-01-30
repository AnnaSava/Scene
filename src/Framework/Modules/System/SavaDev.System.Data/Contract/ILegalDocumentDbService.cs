using SavaDev.Base.Data.Services;
using SavaDev.System.Data.Contract.Models;

namespace SavaDev.System.Data.Contract
{
    public interface ILegalDocumentDbService
    {
        Task<OperationResult<LegalDocumentModel>> Create(LegalDocumentModel model);

        Task<OperationResult<LegalDocumentModel>> CreateTranslation(LegalDocumentModel model);

        Task<OperationResult<LegalDocumentModel>> Update(long id, LegalDocumentModel model);

        Task<OperationResult> Publish(long id);

        Task<OperationResult<LegalDocumentModel>> CreateVersion(LegalDocumentModel model);

        Task<OperationResult> Delete(long id);

        Task<OperationResult> Restore(long id);

        Task<LegalDocumentModel> GetOne(long id);

        Task<LegalDocumentModel> GetActual(string permName, string culture);

        //Task<PageListModel<LegalDocumentModel>> GetAll(ListQueryModel<LegalDocumentFilterModel> query);

        Task<bool> CheckPermNameExists(string permName);

        Task<bool> CheckTranslationExists(string permName, string culture);

        Task<bool> CheckHasAllTranslations(string permName);

        Task<IEnumerable<string>> GetMissingCultures(string permName);
    }
}
