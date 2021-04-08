using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.DataService.Contract.Models
{
    public abstract class BaseUserModel
    {
        public long Id { get; set; }

        public string UserName { get; set; }
    }
}
