using SavaDev.Base.Data.Entities.Interfaces;
using SavaDev.Base.Data.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Scheme.Front.Contract.Models
{
    public class TableViewModel : IModel<Guid>
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Module { get; set; }

        public string Entity { get; set; }

        public virtual ICollection<ColumnViewModel> Columns { get;set; }
    }
}
