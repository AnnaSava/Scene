namespace Framework.Base.DataService.Contract.Models
{
    public class BaseAliasedModel<TKey> : BaseModel<TKey>
    {
        public string Alias { get; set; }
    }
}
