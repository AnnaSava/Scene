using System;

namespace Framework.Base.DataService.Entities
{
    [Obsolete]
    public abstract class BaseAliasedEntity<TKey> : BaseEntityRestorable<TKey>, IEntityAliased
    {
        public string Alias { get; set; }
    }
}
