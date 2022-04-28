using Framework.Base.DataService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.DataService.Entities
{
    public class Consent : BaseEntity<int>, IEntity<int>, IEntityRestorable
    {
        public string Text { get; set; }

        public string Comment { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastUpdated { get; set; }

        public bool IsApproved { get; set; }
    }
}
