using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.Service.Contract.Models
{
    public class FrameworkSignInResultViewModel
    {
        public bool Succeeded { get; set; }

        public FrameworkUserViewModel User { get; set; }
    }
}
