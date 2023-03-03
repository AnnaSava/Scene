using SavaDev.Base.Data.Registry;
using SavaDev.Base.Front.Registry;
using SavaDev.General.Front.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.General.Front.Contract
{
    public interface ILegalDocumentViewService
    {
        Task<TResult> GetOne<TResult>(long id);

        Task<TResult> GetActual<TResult>(string permName, string culture);

        Task<LegalDocumentViewModel> Create(LegalDocumentFormViewModel model);

        Task<LegalDocumentViewModel> CreateTranslation(LegalDocumentFormViewModel model);

        Task<LegalDocumentViewModel> Update(long id, LegalDocumentFormViewModel model);

        Task Publish(long id);

        Task<LegalDocumentViewModel> CreateVersion(LegalDocumentFormViewModel model);

        Task<LegalDocumentViewModel> Delete(long id);

        Task<LegalDocumentViewModel> Restore(long id);

        Task<RegistryPageViewModel<LegalDocumentViewModel>> GetRegistryPage(RegistryQuery query);

        Task<bool> CheckPermNameExists(string permName);

        Task<bool> CheckTranslationExists(string permName, string culture);

        Task<IEnumerable<string>> GetMissingCultures(string permName);
    }
}
