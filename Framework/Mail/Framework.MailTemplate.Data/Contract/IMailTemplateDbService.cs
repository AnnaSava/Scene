using Framework.Base.DataService.Contract.Models;
using Framework.Base.DataService.Contract.Models.ListView;
using Framework.Base.Types.View;
using Framework.MailTemplate.Data.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.MailTemplate.Data.Contract
{
    public interface IMailTemplateDbService
    {
        Task<OperationResult<MailTemplateModel>> Create(MailTemplateModel model);

        Task<OperationResult<MailTemplateModel>> CreateTranslation(MailTemplateModel model);

        Task<OperationResult<MailTemplateModel>> Update(long  id, MailTemplateModel model);

        Task<OperationResult> Publish(long id);

        Task<OperationResult<MailTemplateModel>> CreateVersion(MailTemplateModel model);

        Task<OperationResult> Delete(long id);

        Task<OperationResult> Restore(long id);

        Task<MailTemplateModel> GetOne(long id);

        Task<MailTemplateModel> GetActual(string permName, string culture);

        Task<PageListModel<MailTemplateModel>> GetAll(ListQueryModel<MailTemplateFilterModel> query);

        Task<bool> CheckPermNameExists(string permName);

        Task<bool> CheckTranslationExists(string permName, string culture);

        Task<bool> CheckHasAllTranslations(string permName);

        Task<IEnumerable<string>> GetMissingCultures(string permName);
    }
}
