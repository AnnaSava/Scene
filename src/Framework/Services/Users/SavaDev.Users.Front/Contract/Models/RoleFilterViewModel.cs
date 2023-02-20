using Framework.Base.Service.ListView;
using SavaDev.Base.Data.Registry.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Users.Front.Contract.Models
{
    public class RoleFilterViewModel : BaseFilter
    {
        public string Name { get; set; }
    }
}
