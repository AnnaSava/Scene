﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Users.Front.Contract.Models
{
    public class RoleFormViewModel
    {
        public string Name { get; set; }

        public IEnumerable<string> Permissions { get; set; }
    }
}
