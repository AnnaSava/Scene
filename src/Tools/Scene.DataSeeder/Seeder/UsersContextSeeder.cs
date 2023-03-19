using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SavaDev.Base.Data.Seeder;
using SavaDev.General.Data.Seeder;
using SavaDev.Users.Data;
using SavaDev.Users.Data.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Scene.DataSeeder
{
    internal class UsersContextSeeder : ISeeder
    {
        private readonly UsersContext context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public UsersContextSeeder(UsersContext dbContext,
            UserManager<User> userManager,
            RoleManager<Role> roleManager)
        {
            context = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Seed()
        {
            await context.Database.MigrateAsync();

            var adminExists = context.Users.Any(m => m.UserName == "admin");

            if (!adminExists)
            {
                try
                {
                    var res = _userManager.CreateAsync(new User() { Email = "test@test.ru", UserName = "admin", RegDate = DateTime.UtcNow }, "Pass123$").GetAwaiter().GetResult();
                }
                catch (Exception ex) 
                {
                    var t = ex;
                }
            }

            var admRole = context.Roles.Any(m => m.Name == "administrator");
            var moderRole = context.Roles.Any(m => m.Name == "moderator");
            var editorRole = context.Roles.Any(m => m.Name == "editor");

            if (!admRole) _roleManager.CreateAsync(new Role { Name = "administrator", LastUpdated = DateTime.UtcNow }).GetAwaiter().GetResult();
            if (!moderRole) _roleManager.CreateAsync(new Role { Name = "moderator", LastUpdated = DateTime.UtcNow }).GetAwaiter().GetResult();
            if (!editorRole) _roleManager.CreateAsync(new Role { Name = "editor", LastUpdated = DateTime.UtcNow }).GetAwaiter().GetResult();

            //await new ReservedNameSeeder(context).Seed();
            //await new PermissionSeeder(context).Seed();
            //await new LegalDocumentSeeder(context).Seed();
        }
    }
}
