﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Front.Registry
{
    public abstract class BaseRegistryItemViewModel
    {
        public long Id { get; set; }

        public bool IsSelected { get; set; }
    }
}
