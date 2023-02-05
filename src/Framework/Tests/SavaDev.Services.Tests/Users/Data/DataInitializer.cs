using SavaDev.Users.Data;

namespace SavaDev.Services.Tests.Users.DataTests.Data
{
    internal static class DataInitializer
    {
        public static void FillContextWithUsers(UsersContext context)
        {
            var data = UsersData.GetNewUsers();

            context.Database.EnsureCreated();
            context.Users.AddRange(data);
            context.SaveChanges();
        }

        public static void FillContextWithRoles(UsersContext context)
        {
            var data = RolesData.GetNewRoles();

            context.Database.EnsureCreated();
            context.Roles.AddRange(data);
            context.SaveChanges();
        }
    }
}
