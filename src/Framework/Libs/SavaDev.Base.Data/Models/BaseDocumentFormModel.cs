using SavaDev.Base.Data.Enums;
using SavaDev.Base.Data.Models.Interfaces;

namespace SavaDev.Base.Data.Models
{
    public class BaseDocumentFormModel<TKey> : BaseRestorableModel<TKey>, IFormModel
    {
        public string PermName { get; set; }

        public string Culture { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public DateTime Created { get; set; }

        public DocumentStatus Status { get; set; }
    }
}
