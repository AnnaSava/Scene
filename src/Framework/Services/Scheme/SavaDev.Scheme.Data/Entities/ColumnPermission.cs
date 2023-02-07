using SavaDev.Base.Data.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Scheme.Data.Entities
{
    public class ColumnPermission : IEntity<long>
    {
        public long Id { get; set; }

        public Guid ColumnId { get; set; }

        public virtual Column Column { get; set; }

        public string RoleId { get; set; }

        public bool CanView { get; set; }

        public bool CanUpdate { get; set; }

        public bool CanFilter { get; set; }
    }
}
