using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Scheme.Front.Contract.Models
{
    public class TableConfigViewModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Filter { get; set; }

        public string Columns { get; set; }
    }
}
