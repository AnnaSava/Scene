using Framework.User.DataService.Entities;
using SavaDev.Users.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Services.Tests.Users.DataTests.Data
{
   internal static class UsersData
    {
        internal static IEnumerable<User> GetNewUsers()
        {
            return new List<User>
            {
                new User
                {
                    Id = 1,
                    IsDeleted = false,
                    LockoutEnabled = false,
                    Email = "adm@test.ru",
                    EmailConfirmed = true,
                    PhoneNumber = "1234560",
                    PhoneNumberConfirmed = true,
                    UserName = "adm",
                    FirstName = "admin",
                    LastName = "adminov",
                    DisplayName = "admin",
                    NormalizedUserName = "ADM",
                    NormalizedEmail = "ADM@TEST.RU",
                    SecurityStamp = ""
                },
                new User
                {
                    Id = 2,
                    IsDeleted = false,
                    LockoutEnabled = false,
                    Email = "qwe@test.ru",
                    EmailConfirmed = true,
                    PhoneNumber = "1234561",
                    PhoneNumberConfirmed = false,
                    UserName = "qwe",
                    FirstName = "qwein",
                    LastName = "qweinov",
                    DisplayName = "qwein",
                },
                new User
                {
                    Id = 3,
                    IsDeleted = false,
                    LockoutEnabled = false,
                    Email = "asd@test.ru",
                    EmailConfirmed = false,
                    PhoneNumber = "1234562",
                    PhoneNumberConfirmed = true,
                    UserName = "asd",
                    FirstName = "asdin",
                    LastName = "asdinov",
                    DisplayName = "asdin",
                },
                new User
                {
                    Id = 4,
                    IsDeleted = false,
                    LockoutEnabled = false,
                    Email = "zxc@test.ru",
                    EmailConfirmed = false,
                    PhoneNumber = "1234563",
                    PhoneNumberConfirmed = true,
                    UserName = "zxc",
                    FirstName = "zxcin",
                    LastName = "zxcinov",
                    DisplayName = "zxcin",
                },
                new User
                {
                    Id = 5,
                    IsDeleted = false,
                    LockoutEnabled = false,
                    Email = "qaz@test.ru",
                    EmailConfirmed = true,
                    PhoneNumber = "1234564",
                    PhoneNumberConfirmed = true,
                    UserName = "qaz",
                    FirstName = "qazin",
                    LastName = "qazinov",
                    DisplayName = "qazin",
                },
                new User
                {
                    Id = 6,
                    IsDeleted = false,
                    LockoutEnabled = true,
                    Email = "wsx@test.ru",
                    EmailConfirmed = true,
                    PhoneNumber = "1234565",
                    PhoneNumberConfirmed = false,
                    UserName = "wsx",
                    FirstName = "wsxin",
                    LastName = "wsxinov",
                    DisplayName = "wsxin",
                },
                new User
                {
                    Id = 7,
                    IsDeleted = false,
                    LockoutEnabled = true,
                    Email = "edc@test.ru",
                    EmailConfirmed = true,
                    PhoneNumber = "1234566",
                    PhoneNumberConfirmed = true,
                    UserName = "edc",
                    FirstName = "edcin",
                    LastName = "edcinov",
                    DisplayName = "edcin",
                },
                new User
                {
                    Id = 8,
                    IsDeleted = false,
                    LockoutEnabled = true,
                    Email = "rfv@test.ru",
                    EmailConfirmed = true,
                    PhoneNumber = "1234567",
                    PhoneNumberConfirmed = true,
                    UserName = "rfv",
                    FirstName = "rfvin",
                    LastName = "rfvinov",
                    DisplayName = "rfvin",
                },
                new User
                {
                    Id = 9,
                    IsDeleted = true,
                    LockoutEnabled = false,
                    Email = "tgb@test.ru",
                    EmailConfirmed = true,
                    PhoneNumber = "1234568",
                    PhoneNumberConfirmed = true,
                    UserName = "tgb",
                    FirstName = "tgbin",
                    LastName = "tgbinov",
                    DisplayName = "tgbin",
                },
                new User
                {
                    Id = 10,
                    IsDeleted = true,
                    LockoutEnabled = false,
                    Email = "yhn@test.ru",
                    EmailConfirmed = true,
                    PhoneNumber = "1234569",
                    PhoneNumberConfirmed = true,
                    UserName = "yhn",
                    FirstName = "yhnin",
                    LastName = "yhninov",
                    DisplayName = "yhnin",
                },
            };
        }
    }
}
