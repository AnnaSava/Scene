using SavaDev.Base.Data.Models;
using SavaDev.Base.Data.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Users.Data.Models
{
    public class BaseRoleFormModel : BaseRestorableFormModel<long>, IModel<long>, IFormModel
    {
        public string Name { get; set; }

        public DateTime LastUpdated { get; set; }

        public bool IsDeleted { get; set; }

        public IEnumerable<string> Permissions { get; set; }

    }
}
