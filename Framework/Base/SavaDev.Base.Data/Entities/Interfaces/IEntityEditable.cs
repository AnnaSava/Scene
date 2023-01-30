namespace SavaDev.Base.Data.Entities.Interfaces
{
    public interface IEntityEditable : IAnyEntity
    {
        public DateTime LastUpdated { get; set; }
    }
}
