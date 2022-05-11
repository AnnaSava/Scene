using Framework.Base.DataService.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.DataService.Contract.Models
{
    public class LegalDocumentFilterModel : ListFilterModel
    {
        public WordFilterField PermName { get; set; }

        public WordFilterField Title { get; set; }

        public WordFilterField Culture { get; set; }

        public bool? IsApproved { get; set; }
    }
}
