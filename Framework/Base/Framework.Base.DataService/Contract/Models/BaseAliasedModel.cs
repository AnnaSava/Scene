using Framework.Base.Types.ModelTypes;

namespace Framework.Base.DataService.Contract.Models
{
    public class BaseAliasedModel<TKey> : BaseModel<TKey>, IModelAliased
    {
        public string Alias { get; set; }
    }
}
