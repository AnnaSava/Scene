﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Users.Front.Contract.Models
{
    public class LoginViewModel
    {
        public string Identifier { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
