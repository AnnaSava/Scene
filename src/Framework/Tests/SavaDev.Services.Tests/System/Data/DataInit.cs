using SavaDev.Infrastructure.Util.TestDataGenerator;
using SavaDev.Libs.UnitTestingHelpers;
using SavaDev.System.Data;
using SavaDev.System.Data.Contract.Models;
using SavaDev.System.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Services.Tests.System.Data
{
    internal static class DataInit
    {
        public static List<LegalDocument> LegalDocuments { get; private set; }
        public static List<KeyValuePair<string, LegalDocumentModel>> LegalDocumentsToCreate { get; private set; }

        private static bool IsDataLoaded = false;
        private static ModelFieldsCsvReader FieldsManager = new ModelFieldsCsvReader(new ModelFieldsCsvReaderOptions(), new FieldsTestDataParser());

        public static void ReadAllData()
        {
            LegalDocuments = FieldsManager.GetModels<LegalDocument>("System");
            LegalDocumentsToCreate = FieldsManager.GetModelsWithTestMethods<LegalDocumentModel>("System");
            IsDataLoaded = true;
        }

        public static void FillContextWithEntities(SystemContext context)
        {
            if (!IsDataLoaded)
            {
                ReadAllData();
            }
            TestsInfrastructure.FillContextWithEntities(context, LegalDocuments);
        }

        public static IEnumerable<object[]> GetLegalDocumentsInput(string testName)
        {
            if (!IsDataLoaded)
            {
                ReadAllData();
            }
            return LegalDocumentsToCreate.Where(m => m.Key == testName)
                .Select(m => new object[] { (LegalDocumentModel)m.Value });
        }

        public static IEnumerable<object[]> GetLegalDocumentIdsInput(string testName)
        {
            if (!IsDataLoaded)
            {
                ReadAllData();
            }
            return LegalDocumentsToCreate.Where(m => m.Key == testName)
                .Select(m => new object[] { m.Value.Id });
        }

        public static IEnumerable<object[]> GetLegalDocumentPermNamesInput(string testName)
        {
            if (!IsDataLoaded)
            {
                ReadAllData();
            }
            return LegalDocumentsToCreate.Where(m => m.Key == testName)
                .Select(m => new object[] { m.Value.PermName });
        }
    }
}
