using SavaDev.Base.Data.Models.Enums;
using SavaDev.Base.Data.Registry.Filter;

namespace SavaDev.System.Data.Contract.Models
{
    public class LegalDocumentFilterModel : BaseFilter
    {
        public WordFilterField PermName { get; set; }

        public WordFilterField Title { get; set; }

        public WordFilterField Culture { get; set; }

        public DocumentStatus? Status { get; set; }
    }
}
