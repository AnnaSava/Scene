using Framework.Base.Types;
using Framework.User.DataService.Contract.Interfaces.Context;
using Framework.User.DataService.Entities;
using Framework.User.SeederLib.Data;

namespace Framework.User.SeederLib
{
    public class PermissionSeeder : ISeeder
    {
        private readonly IPermissionContext context;

        public PermissionSeeder(IPermissionContext dbContext)
        {
            context = dbContext;
        }

        public async Task Seed()
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

        public async Task Seed(IEnumerable<string> permissions)
        {
            foreach (var permission in permissions)
            {
                if (context.Permissions.Any(m => m.Name == permission))
                    continue;

                var permissionEntity = new Permission
                {
                    Name = permission,
                    Cultures = new List<PermissionCulture>
                    {
                        new PermissionCulture
                        {
                            PermissionName = permission,
                            Culture = "en",
                            Title = permission
                        },
                        new PermissionCulture
                        {
                            PermissionName = permission,
                            Culture = "ru",
                            Title = permission
                        }
                    }
                };

                context.Permissions.Add(permissionEntity);
                await context.SaveChangesAsync();
            }
        }
    }
}
