﻿namespace Framework.Base.DataService.Entities
{
    public abstract class BaseAliasedEntity<TKey> : BaseEntityRestorable<TKey>, IEntityAliased
    {
        public string Alias { get; set; }
    }
}
