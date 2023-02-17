using SavaDev.Infrastructure.Util.TestDataGenerator;
using SavaDev.Libs.Tests.Base.Data.Managers.Fake;
using SavaDev.Libs.Tests.Base.Data.Services.Fake;
using SavaDev.Libs.UnitTestingHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Libs.Tests.Base.Data.Services
{
    internal class DataInit
    {
        public static List<FakeDocument> FakeDocuments { get; private set; }
        public static List<KeyValuePair<string, FakeDocumentModel>> FakeDocumentsInput { get; private set; }

        private static bool IsDataLoaded = false;
        private static ModelFieldsCsvReader FieldsManager = new ModelFieldsCsvReader(new ModelFieldsCsvReaderOptions(), new FieldsTestDataParser());

        public static void ReadAllData()
        {
            if (IsDataLoaded) return;

            FakeDocuments = FieldsManager.GetModels<FakeDocument>("Base\\Data\\Services\\Fake");
            FakeDocumentsInput = FieldsManager.GetModelsWithTestMethods<FakeDocumentModel>("Base\\Data\\Services\\Fake");
            IsDataLoaded = true;
        }

        public static void FillContextWithEntities(FakeDocumentContext context)
        {
            ReadAllData();
            TestsInfrastructure.FillContextWithEntities(context, FakeDocuments.Select(m => (FakeDocument)m.Clone()));
        }

        public static IEnumerable<object[]> GetFakeDocumentsInput(string testName)
        {
            ReadAllData();
            return FakeDocumentsInput.Where(m => m.Key == testName)
                .Select(m => new object[] { m.Value });
        }

        public static IEnumerable<object[]> GetFakeDocumentIdsInput(string testName)
        {
            ReadAllData();
            return FakeDocumentsInput.Where(m => m.Key == testName)
                .Select(m => new object[] { m.Value.Id });
        }

        public static IEnumerable<object[]> GetFakeDocumentPermNamesInput(string testName)
        {
            ReadAllData();
            return FakeDocumentsInput.Where(m => m.Key == testName)
                .Select(m => new object[] { m.Value.PermName });
        }
    }
}
