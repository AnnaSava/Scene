﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SavaDev.Infrastructure.Reflection;
using SavaDev.General.Data;
using SavaDev.General.Data.Seeder;
using SavaDev.Users.Data.Entities;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using SavaDev.Base.Data.Seeder;

namespace Scene.DataSeeder
{
    internal class SystemContextSeeder : ISeeder
    {
        private readonly GeneralContext context;
        private readonly RoleManager<Role> _roleManager;

        public SystemContextSeeder(GeneralContext dbContext, RoleManager<Role> roleManager)
        {
            context = dbContext;
            _roleManager = roleManager;
        }

        public async Task Seed()
        {
            await context.Database.MigrateAsync();

            await new MailTemplateSeeder(context).Seed();

            await new ReservedNameSeeder(context).Seed();
            await new LegalDocumentSeeder(context).Seed();

            var permissionSeeder = new PermissionSeeder(context);
            await permissionSeeder.Seed();

            var permissionTypes = new Type[] { };

            foreach (var permissionType in permissionTypes)
            {
                var permissions = permissionType.GetAllPublicConstantValues<string>();

                if (permissions.Any())
                {
                    await permissionSeeder.Seed(permissions);

                    var admin = await _roleManager.FindByNameAsync("administrator");
                    var claims = await _roleManager.GetClaimsAsync(admin);
                    var adminPermissions = claims.Where(m => m.Type == "permission").Select(m => m.Value);

                    foreach (var permission in permissions)
                    {
                        if (adminPermissions.Contains(permission) == false)
                        {
                            await _roleManager.AddClaimAsync(admin, new Claim("permission", permission));
                        }
                    }
                }
            }
        }
    }
}
