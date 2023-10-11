using SavaDev.Base.Front.Models;
using SavaDev.Content.Contract.Models;
using SavaDev.Content.Front.Contract.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Content.Front.Contract.Models
{
    public class GetFormViewResult : BaseViewResult
    {
        public FormActions Action { get; set; }

        public long? Id { get; set; }

        public string Content { get; set; }

        public IEnumerable<DraftViewModel> Drafts { get; set; }

        public bool HasMoreDrafts { get; set; }
    }
}
