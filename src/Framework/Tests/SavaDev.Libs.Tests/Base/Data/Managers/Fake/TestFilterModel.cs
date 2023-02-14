using SavaDev.Base.Data.Registry.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Libs.Tests.Base.Data.Managers.Fake
{
    public class TestFilterModel : BaseFilter
    {
        public NumericFilterField<long> Id { get; set; }
    }
}
