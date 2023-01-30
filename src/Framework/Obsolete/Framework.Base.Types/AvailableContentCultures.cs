using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Base.Types
{
    public class AvailableContentCultures
    {
        public string Available { get; set; }

        public string[] GetArr()
        {
            return Available.Split(',');
        }
    }
}
