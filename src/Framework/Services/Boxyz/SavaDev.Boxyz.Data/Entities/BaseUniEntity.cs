using SavaDev.Boxyz.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Boxyz.Data.Entities
{
    public abstract class BaseUniEntity<TEdition> : BaseEntity<long>
    {
        public string Name { get; set; }

        public Guid ActualEditionId { get; set; }

        public virtual TEdition ActualEdition { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastUpdated { get; set; }

        public bool IsDeleted { get;set; }

        public virtual ICollection<TEdition> Editions { get; set; }
    }
}
