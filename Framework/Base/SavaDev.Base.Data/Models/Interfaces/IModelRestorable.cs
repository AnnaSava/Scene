using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Data.Models.Interfaces
{
    public interface IModelRestorable : IModelEditable
    {
        public bool IsDeleted { get; set; }
    }
}
