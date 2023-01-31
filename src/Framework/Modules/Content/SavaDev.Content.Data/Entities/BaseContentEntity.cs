using SavaDev.Base.Data.Entities.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Savadev.Content.Data.Entities
{
    public abstract class BaseContentEntity : IEntity<Guid>
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Entity { get; set; }

        public string Module { get; set; }

        public string OwnerId { get; set; }

        public string Content { get; set; }

        public DateTime Created { get; set; }
    }
}
