using Framework.Base.DataService.Contract.Models;
using Framework.Base.Types.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.MailTemplate.Data.Contract.Models
{
    public class MailTemplateFilterModel : ListFilterModel
    {
        public WordFilterField PermName { get; set; }

        public WordFilterField Title { get; set; }

        public WordFilterField Culture { get; set; }

        public DocumentStatus? Status { get; set; }
    }
}
