using SavaDev.Base.Data.Enums;

namespace SavaDev.Base.Data.Entities
{
    public abstract class BaseDocumentEntity<TKey> : BaseRestorableEntity<TKey>
    {
        public string PermName { get; set; }

        public string Culture { get; set; }

        public string Title { get; set; }

        public string? Text { get; set; }

        public DateTime Created { get; set; }

        public DocumentStatus Status { get; set; }
    }
}
