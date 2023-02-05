using Framework.Base.DataService.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.DataService.Contract.Models
{
    public class UserFilterModel : ListFilterModel
    {
        public WordFilterField Logins { get; set; }

        public WordFilterField Emails { get; set; }

        public bool? EmailConfirmed { get; set; }

        public WordFilterField PhoneNumbers { get; set; }

        public bool? PhoneNumberConfirmed { get; set; }

        public WordFilterField FirstNames { get; set; }

        public WordFilterField LastNames { get; set; }

        public WordFilterField DisplayNames { get; set; }

        public FilterFieldModel<List<DateTime>> RegDates { get; set; } // TODO разобраться с типом данных

        public FilterFieldModel<List<DateTime>> Birthdays { get; set; }   // TODO разобраться с типом данных     

        public bool? IsBanned { get; set; }

        public FilterFieldModel<List<DateTimeOffset>> BanEnds { get; set; } // TODO разобраться с типом данных
    }
}
