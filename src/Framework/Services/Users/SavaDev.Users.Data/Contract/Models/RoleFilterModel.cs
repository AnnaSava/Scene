﻿using SavaDev.Base.Data.Registry.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.DataService.Contract.Models
{
    public class RoleFilterModel : BaseFilter
    {
        public WordFilterField Names { get; set; }
    }
}
