using SavaDev.Base.Data.Models;
using SavaDev.Scheme.Front.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Scheme.Front.Contract
{
    public interface IColumnViewService
    {
        Task<IEnumerable<ColumnViewModel>> GetAll(ModelPlacement modelPlacement);
    }
}
