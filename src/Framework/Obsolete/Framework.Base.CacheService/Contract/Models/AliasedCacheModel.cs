using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Base.CacheService.Contract.Models
{
    public abstract class AliasedCacheModel<TKey> : BaseCacheModel<TKey>
    {
        public string Alias { get; set; }
    }
}
