using SavaDev.Infrastructure.Util.TestDataGenerator;
using SavaDev.Libs.UnitTestingHelpers;
using SavaDev.General.Data;
using SavaDev.General.Data.Contract.Models;
using SavaDev.General.Data.Entities;
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
        public static List<KeyValuePair<string, LegalDocumentModel>> LegalDocumentsInput { get; private set; }
        public static List<Permission> Permissions { get; private set; }
        public static List<KeyValuePair<string, PermissionModel>> PermissionsInput { get; private set; }
        public static List<ReservedName> ReservedNames { get; private set; }
        public static List<KeyValuePair<string, ReservedNameModel>> ReservedNamesInput { get; private set; }

        private static bool IsDataLoaded = false;
        private static ModelFieldsCsvReader FieldsManager = new ModelFieldsCsvReader(new ModelFieldsCsvReaderOptions(), new FieldsTestDataParser());

        public static void ReadAllData()
        {
            if (IsDataLoaded) return;

            LegalDocuments = FieldsManager.GetModels<LegalDocument>("System");
            LegalDocumentsInput = FieldsManager.GetModelsWithTestMethods<LegalDocumentModel>("System");
            Permissions = FieldsManager.GetModels<Permission>("System");
            PermissionsInput = FieldsManager.GetModelsWithTestMethods<PermissionModel>("System");
            ReservedNames = FieldsManager.GetModels<ReservedName>("System");
            ReservedNamesInput = FieldsManager.GetModelsWithTestMethods<ReservedNameModel>("System");
            IsDataLoaded = true;
        }

        public static void FillContextWithEntities(GeneralContext context)
        {
            ReadAllData();
            TestsInfrastructure.FillContextWithEntities(context, LegalDocuments.Select(m => (LegalDocument)m.Clone()));
            TestsInfrastructure.FillContextWithEntities(context, Permissions.Select(m => (Permission)m.Clone()));
            TestsInfrastructure.FillContextWithEntities(context, ReservedNames.Select(m => (ReservedName)m.Clone()));
        }

        public static IEnumerable<object[]> GetLegalDocumentsInput(string testName)
        {
            ReadAllData();
            return LegalDocumentsInput.Where(m => m.Key == testName)
                .Select(m => new object[] { m.Value });
        }

        public static IEnumerable<object[]> GetLegalDocumentIdsInput(string testName)
        {
            ReadAllData();
            return LegalDocumentsInput.Where(m => m.Key == testName)
                .Select(m => new object[] { m.Value.Id });
        }

        public static IEnumerable<object[]> GetLegalDocumentPermNamesInput(string testName)
        {
            ReadAllData();
            return LegalDocumentsInput.Where(m => m.Key == testName)
                .Select(m => new object[] { m.Value.PermName });
        }

        public static IEnumerable<object[]> GetPermissionNamesInput(string testName)
        {
            ReadAllData();
            return PermissionsInput.Where(m => m.Key == testName)
                .Select(m => new object[] { m.Value.Name });
        }

        public static IEnumerable<object[]> GetReservedNamesTextInput(string testName)
        {
            ReadAllData();
            return ReservedNamesInput.Where(m => m.Key == testName)
                .Select(m => new object[] { m.Value.Text });
        }
    }
}
