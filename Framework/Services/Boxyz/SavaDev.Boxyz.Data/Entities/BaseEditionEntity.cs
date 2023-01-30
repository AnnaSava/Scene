using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Boxyz.Data.Entities
{
    public interface IEdition
    {
        public long UniqId { get; set; }
    }

    public class BaseEditionEntity<TUniq> : BaseEntity<Guid>
    {
        public long UniqId { get; set; }

        public virtual TUniq Uniq { get; set; }

        public long StableId { get; set; } 
        
        public DateTime Created { get; set; }

        public bool IsPublished { get; set; }

        public DateTime LastUpdated { get; set; }

        public bool IsDeleted { get; set; }
    }
}
