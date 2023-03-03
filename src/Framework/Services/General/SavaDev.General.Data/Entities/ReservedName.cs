using SavaDev.Base.Data.Entities.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace SavaDev.General.Data.Entities
{
    public class ReservedName : IAnyEntity, ICloneable
    {
        [Key]
        public string Text { get; set; }

        public bool IncludePlural { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
