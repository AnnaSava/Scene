using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Base.DataService.Entities
{
    public class BaseHubEntity
    {
        [Key]
        public Guid Id { get; set; }

        public string OwnerId { get; set; }

        public string AttachedToId { get; set; }

        public string Entity { get; set; }

        public string Module { get; set; }
    }
}
