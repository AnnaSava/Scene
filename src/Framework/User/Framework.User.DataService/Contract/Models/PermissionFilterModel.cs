using Framework.Base.DataService.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.DataService.Contract.Models
{
    public class PermissionFilterModel : IFilter
    {
        public WordFilterField Name { get; set; }
    }
}
