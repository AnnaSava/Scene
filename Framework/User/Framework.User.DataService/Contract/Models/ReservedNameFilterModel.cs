using Framework.Base.DataService.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.DataService.Contract.Models
{
    public class ReservedNameFilterModel : IFilter
    {
        public WordFilterField Text { get; set; }
    }
}
