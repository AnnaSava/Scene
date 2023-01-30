using Framework.Base.DataService.Contract.Models;
using Framework.Base.DataService.Contract.Models.ListView;
using Framework.Base.Types.Registry;
using Framework.User.DataService.Contract.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Framework.User.DataService.Contract.Interfaces
{
    [Obsolete]
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

        Task<PageListModel<LegalDocumentModel>> GetAll(ListQueryModel<LegalDocumentFilterModel> query);

        Task<bool> CheckPermNameExists(string permName);

        Task<bool> CheckTranslationExists(string permName, string culture);

        Task<bool> CheckHasAllTranslations(string permName);

        Task<IEnumerable<string>> GetMissingCultures(string permName);
    }
}
