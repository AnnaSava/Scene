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
        Task<ListPageViewModel<MailTemplateViewModel>> GetAll(MailTemplateFilterViewModel filter, ListPageInfoViewModel pageInfo);

        Task<bool> CheckDocumentExisis(string permName);

        Task<bool> CheckTranslationExisis(string permName, string culture);
    }
}
