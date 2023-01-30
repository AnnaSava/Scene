using Framework.Base.DataService.Entities;
using Framework.Base.Types.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.DataService.Entities
{
    public class LegalDocument : BaseDocumentEntity<long>
    {
        public string Info { get; set; }

        public string Comment { get; set; }
    }
}
