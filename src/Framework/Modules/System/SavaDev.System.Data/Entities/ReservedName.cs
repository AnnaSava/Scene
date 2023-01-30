using SavaDev.Base.Data.Entities.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace SavaDev.System.Data.Entities
{
    public class ReservedName : IAnyEntity
    {
        [Key]
        public string Text { get; set; }

        public bool IncludePlural { get; set; }
    }
}
