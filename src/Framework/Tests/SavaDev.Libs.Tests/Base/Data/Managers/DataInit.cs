using SavaDev.Libs.Tests.Base.Data.Managers.Fake;
using SavaDev.Libs.UnitTestingHelpers;

namespace SavaDev.Libs.Tests.Base.Data.Managers
{
    internal class DataInit
    {
        public static void FillContextWithEntities(FakeContext context)
        {
            var data = FakeData.GetNewEntities();
            TestsInfrastructure.FillContextWithEntities(context, data);
        }
    }
}
