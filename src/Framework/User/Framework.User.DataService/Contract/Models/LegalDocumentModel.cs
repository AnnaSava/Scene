using Framework.Base.DataService.Contract.Models;
using Framework.Base.Types.Enums;
using Framework.Base.Types.ModelTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.DataService.Contract.Models
{
    public class LegalDocumentModel : BaseDocumentFormModel<long>, IModel<long>
    {
        public string PermName { get; set; }

        public string Culture { get; set; }

        public string Title { get; set; }

        public string Info { get; set; }

        public string Text { get; set; }

        public string Comment { get; set; }

        public DateTime Created { get; set; }

        public DocumentStatus Status { get; set; }
    }
}
