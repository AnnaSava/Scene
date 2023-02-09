using SavaDev.Base.Data.Entities.Interfaces;
using SavaDev.Base.Data.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Scheme.Contract.Models
{
    public class FilterModel : IEntity<long>, IFormModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string? OwnerId { get; set; }

        public Guid TableId { get; set; }

        public string Fields { get; set; }
    }
}
