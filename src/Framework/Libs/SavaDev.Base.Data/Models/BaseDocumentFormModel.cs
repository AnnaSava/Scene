using SavaDev.Base.Data.Enums;

namespace SavaDev.Base.Data.Models
{
    public class BaseDocumentFormModel<TKey> : BaseRestorableModel<TKey>
    {
        public string PermName { get; set; }

        public string Culture { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public DateTime Created { get; set; }

        public DocumentStatus Status { get; set; }
    }
}
