using SavaDev.Base.Data.Entities.Interfaces;
using SavaDev.Scheme.Data.Contract.Enums;
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

        public string DataType { get; set; }

        public string TableName { get; set; }

        public Guid TableId { get; set; }

        public virtual Registry Table { get; set; }

        public ColumnDisplay Display { get; set; }

        public bool IsSortable { get; set; }

        public bool HasColumnFilter { get; set; }

        public virtual ICollection<ColumnProperty> Properties { get; set; }

        public virtual ICollection<ColumnPermission> Permissions { get; set; }
    }
}
