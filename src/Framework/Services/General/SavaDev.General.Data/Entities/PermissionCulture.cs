﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.General.Data.Entities.Parts
{
    public class PermissionCulture
    {
        public string PermissionName { get; set; }

        public string Culture { get; set; }

        public string Title { get; set; }

        public virtual Permission Permission { get; set; }
    }
}
