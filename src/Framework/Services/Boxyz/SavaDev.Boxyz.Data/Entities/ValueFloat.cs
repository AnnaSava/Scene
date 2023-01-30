using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Boxyz.Data.Entities
{
    public class ValueFloat : BaseValueEntity
    {
        public double Value { get; set; }

        public string Measure { get; set; }
    }
}
