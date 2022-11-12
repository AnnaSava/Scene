using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.Service.Contract.Models
{
    public class FrameworkRoleFormViewModel
    {
        public string Name { get; set; }

        public IEnumerable<string> Permissions { get; set; }
    }
}
