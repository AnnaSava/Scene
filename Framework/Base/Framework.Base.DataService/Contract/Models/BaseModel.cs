using Framework.Base.Types.ModelTypes;
using System;

namespace Framework.Base.DataService.Contract.Models
{
    public class BaseModel<TKey> : IModelRestorable
    {
        public TKey Id { get; set; }

        public DateTime LastUpdated { get; set; }

        public bool IsDeleted { get; set; }
    }
}
