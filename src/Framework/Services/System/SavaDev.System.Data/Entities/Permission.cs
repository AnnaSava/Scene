using SavaDev.Base.Data.Entities.Interfaces;
using SavaDev.System.Data.Entities.Parts;
using System.ComponentModel.DataAnnotations;

namespace SavaDev.System.Data.Entities
{
    public class Permission : IAnyEntity, ICloneable
    {
        [Key]
        public string Name { get; set; }

        public ICollection<PermissionCulture> Cultures { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
