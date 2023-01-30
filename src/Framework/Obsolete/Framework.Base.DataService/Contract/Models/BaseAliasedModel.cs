using Framework.Base.Types.ModelTypes;
using System;

namespace Framework.Base.DataService.Contract.Models
{
    [Obsolete]
    public class BaseAliasedModel<TKey> : BaseRestorableModel<TKey>, IModelAliased
    {
        public string Alias { get; set; }
    }
}
