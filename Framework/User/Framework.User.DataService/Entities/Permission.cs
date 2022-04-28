using Framework.Base.DataService.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.DataService.Entities
{
    public class Permission : IAnyEntity
    {
        [Key]
        public string Name { get; set; }

        public ICollection<PermissionCulture> Cultures { get; set; }
    }
}
