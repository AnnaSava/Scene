using Framework.Base.Service.ListView;
using Framework.Base.Types.Enums;
using SavaDev.Base.Data.Registry.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.System.Front.Contract.Models
{
    public class MailTemplateFilterViewModel : BaseFilter
    {
        public string PermName { get; set; }

        public string Title { get; set; }

        public string Culture { get; set; }

        public DocumentStatus? Status { get; set; }
    }
}
