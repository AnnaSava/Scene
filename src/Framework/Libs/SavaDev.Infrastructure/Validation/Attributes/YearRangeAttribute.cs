using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Infrastructure.Validation.Attributes
{
    public class YearRangeAttribute : ValidationAttribute
    {
        private const int MinValue = 1800;
        private const int MaxValue = 2100;

        public override bool IsValid(object value)
        {
            if (value == null) return true;
            var t = value.GetType();
            if (!(value.GetType() == typeof(int?) || value.GetType() == typeof(int))) 
                throw new InvalidOperationException("YearRangeAttribute can only be used on int properties.");

            var intValue = (int)value;

            return intValue >= MinValue && intValue <= MaxValue;
        }
    }
}
