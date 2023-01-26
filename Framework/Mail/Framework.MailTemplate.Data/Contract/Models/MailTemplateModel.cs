using Framework.Base.DataService.Contract.Models;
using Framework.Base.Types.Enums;
using Framework.Base.Types.ModelTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.MailTemplate.Data.Contract.Models
{
    public class MailTemplateModel : BaseDocumentFormModel<long>, IModel<long>
    {
        public string PermName { get; set; }

        public string Culture { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Text { get; set; } = string.Empty;

        public DateTime Created { get; set; }

        public DocumentStatus Status { get; set; }
    }
}
