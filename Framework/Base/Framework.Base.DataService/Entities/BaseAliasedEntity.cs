namespace Framework.Base.DataService.Entities
{
    public abstract class BaseAliasedEntity<TKey> : BaseEntity<TKey>, IEntityAliased
    {
        public string Alias { get; set; }
    }
}
