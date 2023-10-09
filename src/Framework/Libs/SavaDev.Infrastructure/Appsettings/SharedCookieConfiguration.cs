using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Infrastructure.Appsettings
{
    public class SharedCookieConfiguration
    {
        public string Name { get; set; }

        public string Domain { get; set; }

        public string SameSite { get; set; }
    }
}
