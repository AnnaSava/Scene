namespace Framework.Base.DataService.Entities
{
    public abstract class BaseEntity<TKey>
    {
        public TKey Id { get; set; }

        public bool IsDeleted { get; set; }
    }
}
