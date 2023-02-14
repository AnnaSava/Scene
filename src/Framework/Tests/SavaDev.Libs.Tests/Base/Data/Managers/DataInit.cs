using SavaDev.Libs.UnitTestingHelpers;
using SavaDev.System.Data.Entities;
using SavaDev.System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SavaDev.Libs.Tests.Base.Data.Managers.Fake;

namespace SavaDev.Libs.Tests.Base.Data.Managers
{
    internal class DataInit
    {
        public static void FillContextWithEntities(TestDbContext context)
        {
            var data = TestData.GetNewEntities();
            TestsInfrastructure.FillContextWithEntities(context, data);
        }
    }
}
