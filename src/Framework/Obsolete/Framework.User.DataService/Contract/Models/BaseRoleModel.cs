using Framework.Base.Types.ModelTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.DataService.Contract.Models
{
  public abstract  class BaseRoleModel : IModel<long>
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public DateTime LastUpdated { get; set; }

        public bool IsDeleted { get; set; }

        public IEnumerable<string> Permissions { get; set; }
    }
}
