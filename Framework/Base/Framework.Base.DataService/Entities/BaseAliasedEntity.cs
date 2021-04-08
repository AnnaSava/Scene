namespace Framework.Base.DataService.Entities
{
    public abstract class BaseAliasedEntity<TKey> : BaseEntity<TKey>
    {
        public string Alias { get; set; }
    }
}
