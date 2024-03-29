﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.User.Data.Models
{
    public class IdentityUserModel
    {
        public long Id { get; set; }

        public string Login { get; set; }

        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public string PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public bool IsBanned { get; set; }

        public DateTimeOffset? BanEnd { get; set; }
    }
}
