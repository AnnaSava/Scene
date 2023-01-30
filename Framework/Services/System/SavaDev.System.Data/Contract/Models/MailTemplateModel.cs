using SavaDev.Base.Data.Models;
using SavaDev.Base.Data.Models.Enums;
using SavaDev.Base.Data.Models.Interfaces;

namespace SavaDev.System.Data.Contract.Models
{
    public class MailTemplateModel : BaseDocumentFormModel<long>, IModel<long>
    {
        public string PermName { get; set; }

        public string Culture { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Text { get; set; } = string.Empty;

        public DateTime Created { get; set; }

        public DocumentStatus Status { get; set; }
    }
}
