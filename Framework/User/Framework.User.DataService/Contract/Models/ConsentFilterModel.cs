using Framework.Base.DataService.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.DataService.Contract.Models
{
    public class ConsentFilterModel : ListFilterModel<int>
    {
        public TextFilterField Text { get; set; }

        public bool? IsApproved { get; set; }
    }
}
