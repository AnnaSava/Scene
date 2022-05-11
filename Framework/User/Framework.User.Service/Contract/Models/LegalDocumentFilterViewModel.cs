using Framework.Base.Service.ListView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.Service.Contract.Models
{
    public class LegalDocumentFilterViewModel : ListFilterViewModel
    {
        public string PermName { get; set; }

        public string Title { get; set; }

        public string Culture { get; set; }

        public bool? IsApproved { get; set; }
    }
}
