using SavaDev.Base.Data.Services;
using SavaDev.Base.Data.Services.Interfaces;
using SavaDev.System.Data.Contract.Models;

namespace SavaDev.System.Data.Contract
{
    public interface ILegalDocumentService : IEntityRegistryService<LegalDocumentModel, LegalDocumentFilterModel>
    {
        Task<OperationResult> Create(LegalDocumentModel model);

        Task<OperationResult> CreateTranslation(LegalDocumentModel model);

        Task<OperationResult> Update(long id, LegalDocumentModel model);

        Task<OperationResult> Publish(long id);

        Task<OperationResult> CreateVersion(LegalDocumentModel model);

        Task<OperationResult> Delete(long id);

        Task<OperationResult> Restore(long id);

        Task<LegalDocumentModel> GetOne<LegalDocumentModel>(long id);

        Task<LegalDocumentModel> GetActual<LegalDocumentModel>(string permName, string culture);

        Task<bool> CheckPermNameExists(string permName);

        Task<bool> CheckTranslationExists(string permName, string culture);

        Task<bool> CheckHasAllTranslations(string permName);

        Task<IEnumerable<string>> GetMissingCultures(string permName);
    }
}
