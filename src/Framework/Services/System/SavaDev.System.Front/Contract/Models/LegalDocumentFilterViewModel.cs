using SavaDev.Base.Data.Enums;
using SavaDev.Base.Data.Registry.Filter;
using SavaDev.Base.Front.Registry;

namespace SavaDev.System.Front.Contract.Models
{
    public class LegalDocumentFilterViewModel : BaseFilter
    {
        public string PermName { get; set; }

        public string Title { get; set; }

        public string Culture { get; set; }

        public DocumentStatus? Status { get; set; }
    }
}
