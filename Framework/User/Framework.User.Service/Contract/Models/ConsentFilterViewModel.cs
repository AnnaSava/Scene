using Framework.Base.Service.ListView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.Service.Contract.Models
{
    public class ConsentFilterViewModel : ListFilterViewModel
    {
        public string Text { get; set; }

        public bool? IsApproved { get; set; }
    }
}
