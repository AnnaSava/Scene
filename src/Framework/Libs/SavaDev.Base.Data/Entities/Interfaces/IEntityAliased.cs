namespace SavaDev.Base.Data.Entities.Interfaces
{
    public interface IEntityAliased : IAnyEntity
    {
        public string Alias { get; set; }
    }
}
