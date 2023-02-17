using SavaDev.Base.Data.Models.Interfaces;
using SavaDev.Base.User.Data.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.User.Data.Models
{
    public abstract class BaseUserModel : IdentityUserModel, IUserModel, IFormModel
    {
        public DateTime LastUpdated { get; set; }

        public bool IsDeleted { get; set; }       
    }
}
