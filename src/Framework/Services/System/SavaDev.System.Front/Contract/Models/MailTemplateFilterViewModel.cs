using SavaDev.Base.Data.Enums;
using SavaDev.Base.Data.Registry.Filter;

namespace SavaDev.System.Front.Contract.Models
{
    public class MailTemplateFilterViewModel : BaseFilter
    {
        public string Id { get; set; }

        public string PermName { get; set; }

        public string Title { get; set; }

        public string Culture { get; set; }

        public DocumentStatus? Status { get; set; }
    }
}
