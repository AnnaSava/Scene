using SavaDev.Base.Data.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Scheme.Data.Entities
{
    public class Column : IEntity<Guid>
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; }

        public Guid TableId { get; set; }

        public virtual Table Table { get; set; }

        public virtual ICollection<ColumnProperty> Properties { get; set; }
    }
}
