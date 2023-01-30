using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.System.Service.Entities
{
   public class EmailTemplate
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string Pattern { get; set; }

        public int Type { get; set; }
    }
}
