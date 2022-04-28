using System;

namespace Framework.Base.DataService.Entities
{
    public abstract class BaseEntity<TKey> : IEntity<TKey>, IEntityRestorable
    {
        public TKey Id { get; set; }

        public DateTime LastUpdated { get; set; }

        public bool IsDeleted { get; set; }
    }
}
