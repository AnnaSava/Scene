using SavaDev.Base.Data.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Scheme.Data.Contract.Models
{
    public class ColumnConfigModel : IModel<long>, IFormModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Columns { get; set; }

        public Guid TableId { get; set; }
    }
}
