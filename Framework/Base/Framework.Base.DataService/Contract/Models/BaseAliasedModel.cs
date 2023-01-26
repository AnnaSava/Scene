using Framework.Base.Types.ModelTypes;

namespace Framework.Base.DataService.Contract.Models
{
    public class BaseAliasedModel<TKey> : BaseRestorableModel<TKey>, IModelAliased
    {
        public string Alias { get; set; }
    }
}
