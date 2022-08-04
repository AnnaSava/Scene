using Framework.Base.Service.ListView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.MailTemplate
{
    public interface IMailTemplateService
    {
        Task<TResult> GetOne<TResult>(long id);

        Task<TResult> GetActual<TResult>(string permName, string culture);

        Task<MailTemplateViewModel> Create(MailTemplateFormViewModel model);

        Task<MailTemplateViewModel> CreateTranslation(MailTemplateFormViewModel model);

        Task<MailTemplateViewModel> Update(long id, MailTemplateFormViewModel model);

        Task Publish(long id);

        Task<MailTemplateViewModel> CreateVersion(MailTemplateFormViewModel model);

        Task<MailTemplateViewModel> Delete(long id);

        Task<MailTemplateViewModel> Restore(long id);

        Task<ListPageViewModel<MailTemplateViewModel>> GetAll(MailTemplateFilterViewModel filter, ListPageInfoViewModel pageInfo);

        Task<bool> CheckPermNameExists(string permName);

        Task<bool> CheckTranslationExists(string permName, string culture);

        Task<IEnumerable<string>> GetMissingCultures(string permName);
    }
}
