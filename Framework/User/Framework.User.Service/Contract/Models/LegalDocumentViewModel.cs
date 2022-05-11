using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.Service.Contract.Models
{
    public class LegalDocumentViewModel
    {
        public long Id { get; set; }

        public string PermName { get; set; }

        public string Culture { get; set; }

        public string Title { get; set; }

        public string Info { get; set; }

        public string Text { get; set; }

        public string Comment { get; set; }

        public DateTime Created { get; set; }

        public bool IsApproved { get; set; }
    }
}
