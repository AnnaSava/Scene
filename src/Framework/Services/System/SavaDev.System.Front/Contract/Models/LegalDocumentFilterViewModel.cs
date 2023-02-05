using SavaDev.Base.Data.Enums;
using SavaDev.Base.Front.Registry;

namespace SavaDev.System.Front.Contract.Models
{
    public class LegalDocumentFilterViewModel : ListFilterViewModel
    {
        public string PermName { get; set; }

        public string Title { get; set; }

        public string Culture { get; set; }

        public DocumentStatus? Status { get; set; }
    }
}
