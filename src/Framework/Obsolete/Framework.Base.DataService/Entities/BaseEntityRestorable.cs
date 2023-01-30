using System;

namespace Framework.Base.DataService.Entities
{
    [Obsolete]
    public abstract class BaseEntityRestorable<TKey> : IEntity<TKey>, IEntityRestorable
    {
        public TKey Id { get; set; }

        public DateTime LastUpdated { get; set; }

        public bool IsDeleted { get; set; }
    }
}
