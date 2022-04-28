using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.DataService.Contract.Models
{
    public abstract class BaseUserModel : IdentityUserModel, IUserModel
    {
        public DateTime LastUpdated { get; set; }

        public bool IsDeleted { get; set; }       
    }
}
