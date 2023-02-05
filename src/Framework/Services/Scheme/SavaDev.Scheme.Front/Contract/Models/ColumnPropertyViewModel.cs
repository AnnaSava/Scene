using SavaDev.Base.Data.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Scheme.Front.Contract.Models
{
    public class ColumnPropertyViewModel : IEntity<long>
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

        public Guid ColumnId { get; set; }

        public virtual ColumnViewModel Column { get; set; }
    }
}
