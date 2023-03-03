using SavaDev.Base.Data.Entities.Interfaces;
using SavaDev.General.Data.Entities.Parts;
using System.ComponentModel.DataAnnotations;

namespace SavaDev.General.Data.Entities
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
