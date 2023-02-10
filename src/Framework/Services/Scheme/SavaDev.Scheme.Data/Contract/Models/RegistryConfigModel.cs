using SavaDev.Base.Data.Models.Interfaces;
using SavaDev.Scheme.Data.Contract.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Scheme.Data.Contract.Models
{
    public class RegistryConfigModel : IModel<long>, IFormModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string? OwnerId { get; set; }

        public string Columns { get; set; }

        public Guid TableId { get; set; }

        public RegistryViewMode ViewMode { get; set; }

        public int ItemsOnPage { get; set; }
    }
}
