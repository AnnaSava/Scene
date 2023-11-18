using SavaDev.Base.Data.Entities.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace SavaDev.Service.Drafts.Entities
{
    public class Draft : IEntity<Guid>, IEntityRestorable
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Entity { get; set; }

        public string Module { get; set; }

        public string OwnerId { get; set; }

        public string Content { get; set; }

        public DateTime Created { get; set; }

        public string? ContentId { get; set; }

        public string? GroupingKey { get; set; }

        public DateTime LastUpdated { get; set; }

        public bool IsDeleted { get; set; }
    }
}