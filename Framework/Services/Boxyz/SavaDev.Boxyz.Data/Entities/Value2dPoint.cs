using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Boxyz.Data.Entities
{
    public class Value2dPoint : BaseValueEntity
    {
        public double X { get; set; }

        public double Y { get; set; }
    }
}
