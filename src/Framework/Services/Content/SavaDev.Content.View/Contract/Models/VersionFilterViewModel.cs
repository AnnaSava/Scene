using SavaDev.Base.Data.Registry.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Content.View.Contract.Models
{
    public class VersionFilterViewModel : BaseFilter
    {
        public string Entity { get; set; }

        public string Module { get; set; }

        public string Owner { get; set; }

        public string ContentId { get; set; }
    }
}
