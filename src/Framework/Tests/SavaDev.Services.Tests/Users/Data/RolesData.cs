using SavaDev.Base.User.Data.Entities;
using SavaDev.Users.Data;

namespace SavaDev.Services.Tests.Users.DataTests.Data
{
    internal static class RolesData
    {
        internal static IEnumerable<Role> GetNewRoles()
        {
            return new List<Role>
            { 
                new Role 
                { 
                    Name = "admin", 
                    NormalizedName = "ADMIN", 
                    Id = 1, 
                    IsDeleted = false 
                },
                new Role
                {
                    Name = "moderator",
                    NormalizedName = "MODERATOR",
                    Id = 2,
                    IsDeleted = false
                }
            };
        }

        internal static IEnumerable<RoleClaim> GetRolePermissions()
        {
            return new List<RoleClaim>
            {
                new RoleClaim
                {
                    ClaimType = "permission",
                    ClaimValue = "user.create",
                    Id = 1,
                    RoleId = 1
                },
                new RoleClaim
                {
                    ClaimType = "permission",
                    ClaimValue = "user.update",
                    Id = 2,
                    RoleId = 1
                },
            };
        }
    }
}
