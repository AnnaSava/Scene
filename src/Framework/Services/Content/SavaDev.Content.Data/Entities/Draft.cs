using SavaDev.Base.Data.Entities.Interfaces;
using SavaDev.Content.Data.Entities;

namespace Savadev.Content.Data.Entities
{
    public class Draft : BaseContentEntity, IEntityRestorable
    {
        public string? ContentId { get; set; }

        public string? GroupingKey { get; set; }

        public DateTime LastUpdated { get; set; }

        public bool IsDeleted { get; set; }
    }
}
