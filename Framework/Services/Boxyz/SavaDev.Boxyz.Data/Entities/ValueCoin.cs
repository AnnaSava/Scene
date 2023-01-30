using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Boxyz.Data.Entities
{
   public class ValueCoin : BaseValueEntity
    {
        public decimal Value { get; set; }

        public string Currency { get; set; }
    }
}
