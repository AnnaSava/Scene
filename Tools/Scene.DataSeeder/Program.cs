﻿using Framework.User.DataService.Entities;
using Framework.User.DataService.Services;
using Framework.User.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scene.DataSeeder.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scene.DataSeeder
{
    class Program
    {
        const string DbConnection = "Host=localhost;Port=5432;Database=SceneDevBase;User Id=postgres;Password=12345678";

        static async Task Main(string[] args)
        {

            Console.WriteLine("Scene Data seeder started!");

            IConfigurationRoot config = null;
            var services = new ServiceCollection();
            services.AddLogging(); // TODO: без него не работает идентити. разобраться

            services.AddDbContext<FrameworkUserDbContext>(options =>
              options.UseNpgsql(DbConnection));
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            services.AddFrameworkUser(config);

            using (ServiceProvider serviceProvider = services.BuildServiceProvider())
            {
                using (IServiceScope scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    await Seed(scope);
                }
            }

            Console.WriteLine("Scene Data seeder finished!");
        }

        private static async Task Seed(IServiceScope scope)
        {
            var context = scope.ServiceProvider.GetService<FrameworkUserDbContext>();
            context.Database.Migrate();

            var adminExists = context.Users.Any(m => m.UserName == "admin");

            if (!adminExists)
            {
                var mgr = scope.ServiceProvider.GetService<UserManager<FrameworkUser>>();

                var res = mgr.CreateAsync(new FrameworkUser() { Email = "test@test.ru", UserName = "admin" }, "Pass123$").GetAwaiter().GetResult();
            }

            var admRole = context.Roles.Any(m => m.Name == "administrator");
            var moderRole = context.Roles.Any(m => m.Name == "moderator");
            var editorRole = context.Roles.Any(m => m.Name == "editor");

            var roleMgr = scope.ServiceProvider.GetService<RoleManager<FrameworkRole>>();

            if (!admRole) roleMgr.CreateAsync(new FrameworkRole { Name = "administrator", LastUpdated = DateTime.Now }).GetAwaiter().GetResult();
            if (!moderRole) roleMgr.CreateAsync(new FrameworkRole { Name = "moderator", LastUpdated = DateTime.Now }).GetAwaiter().GetResult();
            if (!editorRole) roleMgr.CreateAsync(new FrameworkRole { Name = "editor", LastUpdated = DateTime.Now }).GetAwaiter().GetResult();

            await SeedReservedNames(context);
            await SeedConsent(context);
            await SeedPermissions(context);
        }

        private static async Task SeedPermissions(FrameworkUserDbContext context)
        {
            foreach (var permission in PermissionInitialData.PermissionsRuCulture)
            {
                if (context.Permissions.Any(m => m.Name == permission.Key))
                    continue;

                var permissionEntity = new Permission
                {
                    Name = permission.Key,
                    Cultures = new List<PermissionCulture>
                    {
                        new PermissionCulture
                        {
                            PermissionName = permission.Key,
                            Culture = "ru",
                            Title = permission.Value
                        }
                    }
                };

                context.Permissions.Add(permissionEntity);
                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedReservedNames(FrameworkUserDbContext context)
        {
            if (context.ReservedNames.Any()) return;

            var withS = ReservedNameInitialData.WithPlurals.Select(m => new ReservedName { Text = m, IncludePlural = true });
            var withoutS = ReservedNameInitialData.WithoutPlurals.Select(m => new ReservedName { Text = m, IncludePlural = false });

            context.AddRange(withS);
            context.AddRange(withoutS);

            await context.SaveChangesAsync();
        }

        private static async Task SeedConsent(FrameworkUserDbContext context)
        {
            if (context.Consents.Any()) return;

            var consent = new Consent { Text = "Edit this text for you.", Comment = "Initial user consent", Created = DateTime.Now, LastUpdated = DateTime.Now };

            context.Consents.Add(consent);

            await context.SaveChangesAsync();
        }
    }
}
