namespace SavaDev.Base.Data.Entities.Interfaces
{
    public interface IEntity<TKey> : IAnyEntity
    {
        public TKey Id { get; set; }
    }
}
