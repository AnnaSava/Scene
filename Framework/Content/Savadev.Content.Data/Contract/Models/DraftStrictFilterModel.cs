using Framework.Base.DataService.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savadev.Content.Data.Contract.Models
{
    public class DraftStrictFilterModel : ListFilterModel
    {
        public string Entity { get; set; }

        public string Module { get; set; }

        public string OwnerId { get; set; }

        public string ContentId { get; set; }

        public string GroupingKey { get; set; }
    }
}
