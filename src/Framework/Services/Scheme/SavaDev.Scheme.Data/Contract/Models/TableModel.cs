using SavaDev.Base.Data.Entities.Interfaces;
using SavaDev.Base.Data.Models.Interfaces;
using SavaDev.Scheme.Data.Contract.Models;
using SavaDev.Scheme.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Scheme.Contract.Models
{
    public class TableModel : IModel<Guid>, IFormModel
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Module { get; set; }

        public string Entity { get; set; }

        public virtual ICollection<ColumnModel> Columns { get;set; }

        public virtual ICollection<ColumnConfigModel> Configs { get; set; }
    }
}
