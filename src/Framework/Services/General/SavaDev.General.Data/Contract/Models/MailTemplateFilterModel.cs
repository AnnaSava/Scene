using SavaDev.Base.Data.Enums;
using SavaDev.Base.Data.Registry.Filter;

namespace SavaDev.General.Data.Contract.Models
{
    public class MailTemplateFilterModel : BaseFilter
    {
        public WordFilterField PermName { get; set; } = new();

        public WordFilterField Title { get; set; } = new();

        public WordFilterField Culture { get; set; } = new();

        public DocumentStatus? Status { get; set; }
    }
}
