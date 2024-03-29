﻿using SavaDev.Base.Data.Registry.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Users.Data.Contract.Models
{
    public class UserFilterModel : BaseFilter
    {
        public WordFilterField Logins { get; set; }

        public WordFilterField Emails { get; set; }

        public bool? EmailConfirmed { get; set; }

        public WordFilterField PhoneNumbers { get; set; }

        public bool? PhoneNumberConfirmed { get; set; }

        public WordFilterField FirstNames { get; set; }

        public WordFilterField LastNames { get; set; }

        public WordFilterField DisplayNames { get; set; }

        //public FilterFieldModel<List<DateTime>> RegDates { get; set; }

        //public FilterFieldModel<List<DateTime>> Birthdays { get; set; }   

        //public bool? IsBanned { get; set; }

        //public FilterFieldModel<List<DateTime>> BanEnds { get; set; }
    }
}
