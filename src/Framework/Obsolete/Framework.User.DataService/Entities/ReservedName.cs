using Framework.Base.DataService.Entities;
using System.ComponentModel.DataAnnotations;

namespace Framework.User.DataService.Entities
{
    public class ReservedName : IAnyEntity
    {
        [Key]
        public string Text { get; set; }

        public bool IncludePlural { get; set; }
    }
}
