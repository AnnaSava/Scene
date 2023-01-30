using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.System.Service.Contract
{
    public class EmailTemplateModel
    {
        public long Id { get; set; }

        public string Key { get; set; }

        public string Title { get; set; }

        public string Pattern { get; set; }

        public int Type { get; set; }
    }
}
