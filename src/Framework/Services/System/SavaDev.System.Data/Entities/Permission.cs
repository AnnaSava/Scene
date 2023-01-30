using SavaDev.Base.Data.Entities.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace SavaDev.System.Data.Entities
{
    public class Permission : IAnyEntity
    {
        [Key]
        public string Name { get; set; }

        public ICollection<PermissionCulture> Cultures { get; set; }
    }
}
