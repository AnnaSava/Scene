using SavaDev.Base.Data.Models.Interfaces;
using SavaDev.Scheme.Data.Contract.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Scheme.Front.Contract.Models
{
    public class ColumnViewModel : IModel<Guid>
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; }

        public Guid TableId { get; set; }

        public virtual RegistryViewModel Table { get; set; }

        public ColumnDisplay Display { get; set; }

        public bool IsSortable { get; set; }

        public bool HasColumnFilter { get; set; }

        public virtual ICollection<ColumnPropertyViewModel> Properties { get; set; }
    }
}
