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
        Task<PageListModel<MailTemplateModel>> GetAll(ListQueryModel<MailTemplateFilterModel> query);

        Task<bool> CheckDocumentExisis(string permName);

        Task<bool> CheckTranslationExisis(string permName, string culture);
    }
}
