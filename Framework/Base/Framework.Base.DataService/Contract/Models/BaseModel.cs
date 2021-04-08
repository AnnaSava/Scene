namespace Framework.Base.DataService.Contract.Models
{
    public class BaseModel<TKey>
    {
        public TKey Id { get; set; }

        public bool IsDeleted { get; set; }
    }
}
