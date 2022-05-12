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
        Task<LegalDocumentViewModel> GetOne(int id);

        Task<LegalDocumentViewModel> Create(LegalDocumentViewModel model);

        Task<LegalDocumentViewModel> CreateTranslation(LegalDocumentViewModel model);

        Task<LegalDocumentViewModel> Update(LegalDocumentViewModel model);

        Task Approve(long id);

        Task<LegalDocumentViewModel> CreateVersion(LegalDocumentViewModel model);

        Task<LegalDocumentViewModel> Delete(int id);

        Task<LegalDocumentViewModel> Restore(int id);

        Task<ListPageViewModel<LegalDocumentViewModel>> GetAll(LegalDocumentFilterViewModel filter, ListPageInfoViewModel pageInfo);

        Task<bool> CheckDocumentExisis(string permName);

        Task<bool> CheckTranslationExisis(string permName, string culture);
    }
}
