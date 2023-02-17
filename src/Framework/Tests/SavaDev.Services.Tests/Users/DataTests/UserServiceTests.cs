using AutoMapper;
using Framework.User.DataService.Contract.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SavaDev.Base.Data.Exceptions;
using SavaDev.Libs.UnitTestingHelpers;
using SavaDev.Services.Tests.Users;
using SavaDev.Services.Tests.Users.DataTests.Data;
using SavaDev.Users.Data;
using SavaDev.Users.Data.Contract.Models;
using SavaDev.Users.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace SavaDev.Services.Tests.Users.DataTests
{
    public class UserServiceTests : IDisposable
    {
        private IMapper _mapper;
        private UsersContext _context;
        private UserManager<User> _userManager;
        private UserService _userDbService;
        private ILogger<UserService> _logger;

        public UserServiceTests()
        {
            _mapper = Dependencies.GetDataMapper();
            _logger = TestsInfrastructure.GetLogger<UserService>();
            _context = TestsInfrastructure.GetContext<UsersContext>(x => new UsersContext(x));
            _userManager = TestsInfrastructure.GetUserManager(_context);
            DataInit.FillContextWithEntities(_context);
            _userDbService = new UserService(_context, _userManager, _mapper, _logger);
        }

        public void Dispose()
        {
            _mapper = null;
            _context = null;
            _userManager = null;
            _userDbService = null;
            _logger = null;
        }

        [Fact]
        public async Task Create_Ok()
        {
            // Arrange
            var userModel = new UserFormModel() { Login = "any", Email = "any@test.ru" };
            var password = "Pass123$";

            // Act
            var result = await _userDbService.Create(userModel, password);

            // Assert
            Assert.IsType<UserModel>(result.ProcessedObject);
            var model = result.ProcessedObject as UserModel;
            Assert.Equal(userModel.Login, model.Login);           
        }

        [Fact]
        public async Task Update_Ok()
        {
            // Arrange
            var userModel = new UserFormModel() { Id = 1, Login = "admin" };
            var updated = DateTime.UtcNow;

            // Act
            var result = await _userDbService.Update(userModel.Id, userModel);

            // Assert
            Assert.IsType<UserModel>(result.ProcessedObject);
            var model = result.ProcessedObject as UserModel;
            Assert.Equal(userModel.Login, model.Login);
            Assert.True(updated < model.LastUpdated);
        }

        [Fact]
        public async Task Update_NotFound()
        {
            // Arrange
            var userModel = new UserFormModel() { Id = 100, Login = "admin" };

            // Act
            async Task action() => await _userDbService.Update(userModel.Id, userModel);

            // Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(action);
        }

        [Fact]
        public async Task Delete_Ok()
        {
            // Arrange
            long id = 1;
            var updated = DateTime.UtcNow;

            // Act
            var result = await _userDbService.Delete(id);

            // Assert
            Assert.IsType<UserModel>(result.ProcessedObject);
            var model = result.ProcessedObject as UserModel;
            Assert.True(model.IsDeleted);
            Assert.True(updated < model.LastUpdated);
        }

        [Fact]
        public async Task Restore_Ok()
        {
            // Arrange
            long id = 1;
            var updated = DateTime.UtcNow;

            // Act
            var result = await _userDbService.Restore(id);

            // Assert
            Assert.IsType<UserModel>(result.ProcessedObject);
            var model = result.ProcessedObject as UserModel;
            Assert.False(model.IsDeleted);
            Assert.True(updated < model.LastUpdated);
        }

        [Theory]
        [MemberData(nameof(Data_LoginExists))]
        public async Task CheckLoginExists_True(string login)
        {
            // Arrange

            // Act
            var result = await _userDbService.CheckLoginExists(login);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [MemberData(nameof(Data_LoginNotExists))]
        public async Task CheckLoginExists_False(string login)
        {
            // Arrange

            // Act
            var result = await _userDbService.CheckLoginExists(login);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [MemberData(nameof(Data_EmailExists))]
        public async Task CheckEmailExists_True(string email)
        {
            // Arrange

            // Act
            var result = await _userDbService.CheckEmailExists(email);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [MemberData(nameof(Data_EmailNotExists))]
        public async Task CheckEmailExists_False(string email)
        {
            // Arrange

            // Act
            var result = await _userDbService.CheckEmailExists(email);

            // Assert
            Assert.False(result);
        }

        public static IEnumerable<object[]> Data_LoginExists => new List<object[]>
        {
            new object[] { "adm" },
            new object[] { "Adm" },
        };

        public static IEnumerable<object[]> Data_LoginNotExists => new List<object[]>
        {
            new object[] { "nothing" },
            new object[] { "" },
            new object[] { null },
        };

        public static IEnumerable<object[]> Data_EmailExists => new List<object[]>
        {
            new object[] { "adm@test.ru" },
            new object[] { "Adm@test.ru" },
        };

        public static IEnumerable<object[]> Data_EmailNotExists => new List<object[]>
        {
            new object[] { "nothing@no.net" },
            new object[] { "" },
            new object[] { null },
        };
    }
}
