using Framework.Base.Types.ModelTypes;
using System;

namespace Framework.Base.DataService.Contract.Models
{
    public class BaseRestorableModel<TKey> : IModelRestorable, IModel<TKey>
    {
        public TKey Id { get; set; }

        public DateTime LastUpdated { get; set; }

        public bool IsDeleted { get; set; }
    }
}
