using Framework.Base.Service.ListView;
using Framework.User.DataService.Contract.Models;
using Framework.User.Service.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.Service.Contract.Interfaces
{
    public interface ILegalDocumentService
    {
        Task<TResult> GetOne<TResult>(long id);

        Task<TResult> GetActual<TResult>(string permName, string culture);

        Task<LegalDocumentViewModel> Create(LegalDocumentFormViewModel model);

        Task<LegalDocumentViewModel> CreateTranslation(LegalDocumentFormViewModel model);

        Task<LegalDocumentViewModel> Update(long id, LegalDocumentFormViewModel model);

        Task Approve(long id);

        Task<LegalDocumentViewModel> CreateVersion(LegalDocumentFormViewModel model);

        Task<LegalDocumentViewModel> Delete(int id);

        Task<LegalDocumentViewModel> Restore(int id);

        Task<ListPageViewModel<LegalDocumentViewModel>> GetAll(LegalDocumentFilterViewModel filter, ListPageInfoViewModel pageInfo);

        Task<bool> CheckDocumentExisis(string permName);

        Task<bool> CheckTranslationExisis(string permName, string culture);
    }
}
