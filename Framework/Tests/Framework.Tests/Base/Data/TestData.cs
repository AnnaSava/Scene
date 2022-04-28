using Framework.Base.DataService.Services;
using Framework.User.DataService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Tests.Base.Data
{
    internal class TestData
    {
        internal static IEnumerable<TestEntity> GetNewEntities()
        {
            return new List<TestEntity>
            {
                new TestEntity
                {
                    Id = 1,
                    IsDeleted = false,
                },
                new TestEntity
                {
                    Id = 2,
                    IsDeleted = false,
                },
                new TestEntity
                {
                    Id = 3,
                    IsDeleted = false,
                },
                new TestEntity
                {
                    Id = 4,
                    IsDeleted = false,
                },
                new TestEntity
                {
                    Id = 5,
                    IsDeleted = false,
                },
                new TestEntity
                {
                    Id = 6,
                    IsDeleted = false,
                },
                new TestEntity
                {
                    Id = 7,
                    IsDeleted = false,
                },
                new TestEntity
                {
                    Id = 8,
                    IsDeleted = false,
                },
                new TestEntity
                {
                    Id = 9,
                    IsDeleted = true,
                },
                new TestEntity
                {
                    Id = 10,
                    IsDeleted = true,
                },
            };
        }

        internal static void ApplyFilters(ref IQueryable<TestEntity> list, TestFilterModel filter)
        {
            list = list.ApplyIdFilter(filter).ApplyIsDeletedFilter(filter);
        }

        internal static IEnumerable<ReservedName> GetReservedNames()
        {
            return new List<ReservedName>
            {                
                new ReservedName
                {
                    Text = "admin",
                    IncludePlural = true,
                },
                new ReservedName
                {
                    Text = "moderator",
                    IncludePlural = true,
                },
                new ReservedName
                {
                    Text = "create",
                    IncludePlural = false
                },
                new ReservedName
                {
                    Text = "drop",
                    IncludePlural = true,
                }
            };
        }

        internal static IEnumerable<Permission> GetPermissions()
        {
            return new List<Permission>
            { 
                new Permission
                {
                    Name = "user.create",
                    Cultures = new List<PermissionCulture> { new PermissionCulture { PermissionName = "user.create", Culture = "ru", Title = "Создание пользователя" } }
                },
                new Permission
                {
                    Name = "user.update",
                    Cultures = new List<PermissionCulture> { new PermissionCulture { PermissionName = "user.update", Culture = "ru", Title = "Редактирование пользователя" } }
                },
                new Permission
                {
                    Name = "user.delete",
                    Cultures = new List<PermissionCulture> { new PermissionCulture { PermissionName = "user.delete", Culture = "ru", Title = "Удаление пользователя" } }
                }
            };
        }
    }
}
