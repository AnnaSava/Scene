using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Front.Models
{
    public class BaseViewResult
    {
        public bool Success { get; set; } = true;

        public string ErrMessage { get; set; }
    }
}
