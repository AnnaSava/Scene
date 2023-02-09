using SavaDev.Base.Data.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Scheme.Data.Entities
{
    public class Filter : IEntity<long>
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string? OwnerId { get; set; }

        public Guid TableId { get; set; }

        public virtual Table Table { get; set; }

        public string Fields { get; set; }
    }
}
