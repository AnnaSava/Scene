using Framework.Base.DataService.Contract.Models;
using Framework.Base.DataService.Contract.Models.ListView;
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
        Task<MailTemplateModel> Create(MailTemplateModel model);

        Task<MailTemplateModel> CreateTranslation(MailTemplateModel model);

        Task<MailTemplateModel> Update(MailTemplateModel model);

        Task Publish(long id);

        Task<MailTemplateModel> CreateVersion(MailTemplateModel model);

        Task<MailTemplateModel> Delete(long id);

        Task<MailTemplateModel> Restore(long id);

        Task<MailTemplateModel> GetOne(long id);

        Task<MailTemplateModel> GetActual(string permName, string culture);

        Task<PageListModel<MailTemplateModel>> GetAll(ListQueryModel<MailTemplateFilterModel> query);

        Task<bool> CheckPermNameExists(string permName);

        Task<bool> CheckTranslationExists(string permName, string culture);

        Task<bool> CheckHasAllTranslations(string permName);

        Task<IEnumerable<string>> GetMissingCultures(string permName);
    }
}
