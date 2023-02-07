using SavaDev.Base.Data.Models.Interfaces;
using SavaDev.Scheme.Data.Contract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Scheme.Contract.Models
{
    public class ColumnModel : IModel<Guid>, IFormModel
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; }

        public Guid TableId { get; set; }

        public virtual TableModel Table { get; set; }

        public bool IsDisplayed { get; set; }

        public bool IsSortable { get; set; }

        public bool HasColumnFilter { get; set; }

        public virtual ICollection<ColumnPropertyModel> Properties { get; set; }

        public virtual ICollection<ColumnPermissionModel> Permissions { get; set; }
    }
}
