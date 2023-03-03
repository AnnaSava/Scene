using SavaDev.Base.Front.Registry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.General.Front.Contract.Models
{
    public class PermissionViewModel : BaseRegistryItemViewModel<long>
    {
        public string Name { get; set; }

        public bool IsChecked { get; set; }
    }
}
