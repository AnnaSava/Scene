using Microsoft.Win32;
using SavaDev.Base.Data.Entities.Interfaces;
using SavaDev.Scheme.Data.Contract.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Scheme.Data.Entities
{
    public class RegistryConfig : IEntity<long>
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string? OwnerId { get; set; }

        public Guid TableId { get; set; }

        public virtual Registry Table { get; set; }

        public string Columns { get; set; }

        public RegistryViewMode ViewMode { get; set; }

        public int ItemsOnPage { get; set; }
    }
}
