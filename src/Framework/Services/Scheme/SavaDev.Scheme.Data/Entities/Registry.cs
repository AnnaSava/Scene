﻿using SavaDev.Base.Data.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Scheme.Data.Entities
{
    public class Registry : IEntity<Guid>
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Module { get; set; }

        public string Entity { get; set; }

        public virtual ICollection<Column> Columns { get;set; }
        
        public virtual ICollection<RegistryConfig> Configs { get;set; }

        public virtual ICollection<Filter> Filters { get;set; }
    }
}
