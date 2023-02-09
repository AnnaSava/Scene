using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Scheme.Front.Contract.Models
{
    public class ColumnConfigViewModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string? OwnerId { get; set; }

        public Guid TableId { get; set; }

        public string Columns { get; set; }

        public List<Guid> ColumnIds { get; set; }

        public bool ForAll { get; set; }
    }
}
