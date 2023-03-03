using SavaDev.Libs.UnitTestingHelpers;
using SavaDev.General.Data.Entities;
using SavaDev.General.Data;
using SavaDev.Users.Data;
using SavaDev.Infrastructure.Util.TestDataGenerator;
using SavaDev.General.Data.Contract.Models;
using Framework.User.DataService.Contract.Models;
using SavaDev.Users.Data.Entities;
using SavaDev.Users.Data.Contract.Models;

namespace SavaDev.Services.Tests.Users.DataTests.Data
{
    internal static class DataInit
    {
        public static List<User> Users { get; private set; }
        public static List<KeyValuePair<string, UserModel>> UsersInput { get; private set; }
        public static List<Role> Roles { get; private set; }
        public static List<KeyValuePair<string, RoleModel>> RolesInput { get; private set; }

        private static bool IsDataLoaded = false;
        private static ModelFieldsCsvReader FieldsManager = new ModelFieldsCsvReader(new ModelFieldsCsvReaderOptions(), new FieldsTestDataParser());


        public static void ReadAllData()
        {
            if (IsDataLoaded) return;

            Users = FieldsManager.GetModels<User>("Users");
            UsersInput = FieldsManager.GetModelsWithTestMethods<UserModel>("Users");
            Roles = FieldsManager.GetModels<Role>("Users");
            RolesInput = FieldsManager.GetModelsWithTestMethods<RoleModel>("Users");
            IsDataLoaded = true;
        }

        public static void FillContextWithEntities(UsersContext context)
        {
            ReadAllData();
            TestsInfrastructure.FillContextWithEntities(context, Users.Select(m => (User)m.Clone()));
            TestsInfrastructure.FillContextWithEntities(context, Roles.Select(m => (Role)m.Clone()));
        }
    }
}
