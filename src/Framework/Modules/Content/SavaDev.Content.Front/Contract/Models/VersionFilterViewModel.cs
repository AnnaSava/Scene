using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savadev.Content.Contract.Models
{
    public class VersionFilterViewModel
    {
        public string Entity { get; set; }

        public string Module { get; set; }

        public string Owner { get; set; }

        public string ContentId { get; set; }
    }
}
