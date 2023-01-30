using SavaDev.Base.Data.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Data.Entities
{
    public abstract class BaseRestorableEntity<TKey> : IEntity<TKey>, IEntityRestorable
    {
        public TKey Id { get; set; }

        public DateTime LastUpdated { get; set; }

        public bool IsDeleted { get; set; }
    }
}
