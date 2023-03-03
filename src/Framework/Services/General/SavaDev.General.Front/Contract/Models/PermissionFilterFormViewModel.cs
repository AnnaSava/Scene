using SavaDev.Base.Data.Registry.Filter;
using SavaDev.Base.Front.Registry.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.General.Front.Contract.Models
{
    public class PermissionFilterFormViewModel : BaseFilter
    {
        public WordField Name { get; set; } = new WordField();
    }
}
