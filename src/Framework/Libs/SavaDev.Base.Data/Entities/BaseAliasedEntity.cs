using SavaDev.Base.Data.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Data.Entities
{
    public abstract class BaseAliasedEntity<TKey> : BaseRestorableEntity<TKey>, IEntityAliased
    {
        public string Alias { get; set; }
    }
}
