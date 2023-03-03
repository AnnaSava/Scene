using SavaDev.Base.Front.Registry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.General.Front.Contract.Models
{
    public class ReservedNameViewModel : BaseRegistryItemViewModel<long>
    {
        public string Text { get; set; }

        public bool IncludePlural { get; set; }
    }
}
