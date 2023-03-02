using SavaDev.Base.Data.Registry.Filter;
using SavaDev.Base.Front.Registry.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.System.Front.Contract.Models
{
    public class ReservedNameFilterFormViewModel : BaseFilter
    {
        public WordField Text { get; set; } = new WordField();
    }
}
