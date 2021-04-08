using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Base.CacheService.Contract.Models
{
    public abstract class BaseCacheModel<TKey>
    {
        public TKey Id { get; set; }

        public bool IsDeleted { get; set; }

        public string CacheId { get; set; }
    }
}
