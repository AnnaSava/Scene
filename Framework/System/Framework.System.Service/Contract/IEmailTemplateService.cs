using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.System.Service.Contract
{
    public interface IEmailTemplateService
    {
        Task<IEnumerable<EmailTemplateModel>> GetAll();

        Task<EmailTemplateModel> GetOneByKey(string key);
    }
}
