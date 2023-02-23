using SavaDev.Base.Data.Enums;
using SavaDev.Base.Data.Registry.Filter;

namespace SavaDev.System.Data.Contract.Models
{
    public class LegalDocumentFilterModel : BaseFilter
    {
        public NumericFilterField<long> Id { get; set; }

        public WordFilterField PermName { get; set; }

        public WordFilterField Title { get; set; }

        public WordFilterField Culture { get; set; }

        public EnumFilterField Status { get; set; } = new EnumFilterField();
    }
}
