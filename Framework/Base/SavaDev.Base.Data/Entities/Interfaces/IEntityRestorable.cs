namespace SavaDev.Base.Data.Entities.Interfaces
{
    public interface IEntityRestorable : IEntityEditable
    {
        public bool IsDeleted { get; set; }
    }
}
