using SavaDev.Base.Front.Models;
using SavaDev.Content.View.Contract.Enums;

namespace SavaDev.Content.View.Contract.Models
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
