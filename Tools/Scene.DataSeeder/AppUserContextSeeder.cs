using Framework.Base.Types;
using Framework.User.DataService.Entities;
using Framework.User.DataService.Services;
using Framework.User.SeederLib;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scene.DataSeeder
{
    internal class AppUserContextSeeder : ISeeder
    {
        private readonly FrameworkUserDbContext context;
        private readonly UserManager<FrameworkUser> _userManager;
        private readonly RoleManager<FrameworkRole> _roleManager;

        public AppUserContextSeeder(FrameworkUserDbContext dbContext,
            UserManager<FrameworkUser> userManager,
            RoleManager<FrameworkRole> roleManager)
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
                var res = _userManager.CreateAsync(new FrameworkUser() { Email = "test@test.ru", UserName = "admin" }, "Pass123$").GetAwaiter().GetResult();
            }

            var admRole = context.Roles.Any(m => m.Name == "administrator");
            var moderRole = context.Roles.Any(m => m.Name == "moderator");
            var editorRole = context.Roles.Any(m => m.Name == "editor");

            if (!admRole) _roleManager.CreateAsync(new FrameworkRole { Name = "administrator", LastUpdated = DateTime.Now }).GetAwaiter().GetResult();
            if (!moderRole) _roleManager.CreateAsync(new FrameworkRole { Name = "moderator", LastUpdated = DateTime.Now }).GetAwaiter().GetResult();
            if (!editorRole) _roleManager.CreateAsync(new FrameworkRole { Name = "editor", LastUpdated = DateTime.Now }).GetAwaiter().GetResult();

            await new ReservedNameSeeder(context).Seed();
            await new PermissionSeeder(context).Seed();
            await new LegalDocumentSeeder(context).Seed();
        }
    }
}
