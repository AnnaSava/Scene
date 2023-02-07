﻿using SavaDev.Base.Data.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Scheme.Data.Entities
{
    public class TableConfig : IEntity<long>
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public Guid TableId { get; set; }

        public virtual Table Table { get; set; }

        public string Filter { get; set; }

        public string Columns { get; set; }
    }
}
