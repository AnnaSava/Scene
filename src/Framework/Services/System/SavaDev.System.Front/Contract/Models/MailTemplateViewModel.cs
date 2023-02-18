using SavaDev.Base.Data.Enums;
using SavaDev.Base.Front.Registry;

namespace SavaDev.System.Front.Contract.Models
{
    public class MailTemplateViewModel : BaseRegistryItemViewModel
    {
        public long Id { get; set; }

        public string PermName { get; set; }

        public string Culture { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public DateTime Created { get; set; }

        public DocumentStatus Status { get; set; }

        public bool HasAllTranslations { get; set; }

        public bool IsDeleted { get; set; }
    }
}
