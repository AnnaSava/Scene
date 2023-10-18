using SavaDev.Base.Data.Registry.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Content.View.Contract.Models
{
    public class DraftFilterViewModel : BaseFilter
    {
        public string Entity { get; set; }

        public string Module { get; set; }

        public string Owner { get; set; }

        public string ContentId { get; set; }

        public string GroupingKey { get; set; }

        public bool IsDeleted { get; set; }
    }
}
