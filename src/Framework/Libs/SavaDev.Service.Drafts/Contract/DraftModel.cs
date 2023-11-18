using SavaDev.Base.Data.Models.Interfaces;

namespace SavaDev.Service.Drafts
{
    public class DraftModel<T> : IFormModel, IAnyModel
    {
        public T Id { get; set; }

        public string Entity { get; set; }

        public string Module { get; set; }

        public string OwnerId { get; set; }

        public T Content { get; set; }

        public DateTime Created { get; set; }
        public string? ContentId { get; set; }

        public string? GroupingKey { get; set; }

        public DateTime LastUpdated { get; set; }

        public bool IsDeleted { get; set; }
    }

    public class DraftModel : DraftModel<string>
    {
    }
}
