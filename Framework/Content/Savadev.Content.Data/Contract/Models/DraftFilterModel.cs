using Framework.Base.DataService.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savadev.Content.Data.Contract.Models
{
    public class DraftFilterModel : ListFilterModel<Guid>
    {
        public WordFilterField Entity { get; set; }

        public WordFilterField Module { get; set; }

        public WordFilterField Owner { get; set; }

        public WordFilterField ContentId { get; set; }

        public WordFilterField GroupingKey { get; set; }
    }
}
