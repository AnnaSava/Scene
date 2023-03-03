using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SavaDev.Base.Data.Exceptions;
using SavaDev.Libs.UnitTestingHelpers;
using SavaDev.Services.Tests.System.Data;
using SavaDev.Services.Tests.Users;
using SavaDev.General.Data.Services;
using SavaDev.General.Data;
using SavaDev.Users.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using SavaDev.Services.Tests.Users.DataTests.Data;
using SavaDev.Users.Data.Entities;
using SavaDev.Users.Data.Contract.Models;

namespace SavaDev.Services.Tests.Users.DataTests
{
    public class RoleServiceTests : IDisposable
    {
        private IMapper _mapper;
        private UsersContext _context;
        private RoleManager<Role> _roleManager;
        private RoleService _roleDbService;
        private ILogger<RoleService> _logger;

        public RoleServiceTests()
        {
            _mapper = Dependencies.GetDataMapper();
            _logger = TestsInfrastructure.GetLogger<RoleService>();
            _context = TestsInfrastructure.GetContext<UsersContext>(x => new UsersContext(x));
            _roleManager = TestsInfrastructure.GetRoleManager(_context);
            Data.DataInit.FillContextWithEntities(_context);
            _roleDbService = new RoleService(_context, _roleManager, _mapper, _logger);
        }

        public void Dispose()
        {
            _mapper = null;
            _context = null;
            _roleManager = null;
            _roleDbService = null;
            _logger = null;
        }

        [Fact]
        public async Task Create_Ok()
        {
            // Arrange
            var roleModel = new RoleModel() { Name = "any", Permissions = new List<string> { "user.create" } };

            // Act
            var result = await _roleDbService.Create(roleModel);

            // Assert
            Assert.IsType<RoleModel>(result.ProcessedObject);
            var model = result.ProcessedObject as RoleModel;
            Assert.Equal(roleModel.Name, model.Name);
            Assert.Equal(1, _context.RoleClaims.Count(m => m.RoleId == model.Id && m.ClaimType == "permission" && m.ClaimValue == "user.create"));
        }

        [Fact]
        public async Task Create_AlreadyExists()
        {
            // Arrange
            var roleModel = new RoleModel() { Name = "admin", Permissions = new List<string> { "user.create" } };

            // Act
            async Task action() => await _roleDbService.Create(roleModel);

            // Assert
            await Assert.ThrowsAsync<EntityAlreadyExistsException>(action);
        }

        [Fact]
        public async Task Create_NoPermissions_Ok()
        {
            // Arrange
            var roleModel = new RoleModel() { Name = "any" };

            // Act
            var result = await _roleDbService.Create(roleModel);

            // Assert
            Assert.IsType<RoleModel>(result.ProcessedObject);
            var model = result.ProcessedObject as RoleModel;
            Assert.Equal(roleModel.Name, model.Name);
            Assert.False(_context.RoleClaims.Any(m => m.RoleId == model.Id && m.ClaimType == "permission"));
        }

        [Fact]
        public async Task Create_DuplicatePermissions_Distinct_Ok()
        {
            // Arrange
            var roleModel = new RoleModel() { Name = "any", Permissions = new List<string> { "user.create", "user.create" } };

            // Act
            var result = await _roleDbService.Create(roleModel);

            // Assert
            Assert.IsType<RoleModel>(result.ProcessedObject);
            var model = result.ProcessedObject as RoleModel;
            Assert.Equal(roleModel.Name, model.Name);
            Assert.Equal(1, _context.RoleClaims.Count(m => m.RoleId == model.Id && m.ClaimType == "permission" && m.ClaimValue == "user.create"));
        }

        [Fact]
        public async Task Update_Ok()
        {
            // Arrange
            var roleModel = new RoleModel() { Id = 1, Name = "new admin", Permissions = new List<string> { "user.create", "user.delete" } };

            // Act
            var result = await _roleDbService.Update(roleModel.Id, roleModel);

            // Assert
            Assert.IsType<RoleModel>(result.ProcessedObject);
            var model = result.ProcessedObject as RoleModel;
            Assert.Equal(roleModel.Id, model.Id);
            Assert.Equal(roleModel.Name, model.Name);
            Assert.Equal(1, _context.RoleClaims.Count(m => m.RoleId == model.Id && m.ClaimType == "permission" && m.ClaimValue == "user.create"));
            Assert.Equal(1, _context.RoleClaims.Count(m => m.RoleId == model.Id && m.ClaimType == "permission" && m.ClaimValue == "user.delete"));
            Assert.Equal(0, _context.RoleClaims.Count(m => m.RoleId == model.Id && m.ClaimType == "permission" && m.ClaimValue == "user.update"));
        }

        [Fact]
        public async Task Update_NotFound()
        {
            // Arrange
            var roleModel = new RoleModel() { Id = 100, Name = "new admin" };

            // Act
            async Task action() => await _roleDbService.Update(roleModel.Id, roleModel);

            // Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(action);
        }

        [Fact]
        public async Task Update_NameAlreadyExists()
        {
            // Arrange
            var roleModel = new RoleModel() { Id = 1, Name = "moderator" };

            // Act
            async Task action() => await _roleDbService.Update(roleModel.Id, roleModel);

            // Assert
            await Assert.ThrowsAsync<EntityAlreadyExistsException>(action);
        }

        [Fact]
        public async Task Update_NameNotChanged_Ok()
        {
            // Arrange
            var roleModel = new RoleModel() { Id = 1, Name = "admin", Permissions = new List<string> { "user.create", "user.delete" } };

            // Act
            var result = await _roleDbService.Update(roleModel.Id, roleModel);

            // Assert
            Assert.IsType<RoleModel>(result.ProcessedObject);
            var model = result.ProcessedObject as RoleModel;
            Assert.Equal(roleModel.Id, model.Id);
            Assert.Equal(roleModel.Name, model.Name);
        }

        [Fact]
        public async Task Update_NoPermissions_Ok()
        {
            // Arrange
            var roleModel = new RoleModel() { Id = 1, Name = "new admin" };

            // Act
            var result = await _roleDbService.Update(roleModel.Id, roleModel);

            // Assert
            Assert.IsType<RoleModel>(result.ProcessedObject);
            var model = result.ProcessedObject as RoleModel;
            Assert.Equal(roleModel.Id, model.Id);
            Assert.Equal(roleModel.Name, model.Name);
            Assert.False(_context.RoleClaims.Any(m => m.RoleId == model.Id && m.ClaimType == "permission"));
        }

        [Fact]
        public async Task Update_DuplicatePermissions_Distinct_Ok()
        {
            // Arrange
            var roleModel = new RoleModel() { Id = 1, Name = "new admin", Permissions = new List<string> { "user.create", "user.create", "user.delete", "user.delete" } };

            // Act
            var result = await _roleDbService.Update(roleModel.Id, roleModel);

            // Assert
            Assert.IsType<RoleModel>(result.ProcessedObject);
            var model = result.ProcessedObject as RoleModel;
            Assert.Equal(roleModel.Id, model.Id);
            Assert.Equal(roleModel.Name, model.Name);
            Assert.Equal(1, _context.RoleClaims.Count(m => m.RoleId == model.Id && m.ClaimType == "permission" && m.ClaimValue == "user.create"));
            Assert.Equal(1, _context.RoleClaims.Count(m => m.RoleId == model.Id && m.ClaimType == "permission" && m.ClaimValue == "user.delete"));
            Assert.Equal(0, _context.RoleClaims.Count(m => m.RoleId == model.Id && m.ClaimType == "permission" && m.ClaimValue == "user.update"));
        }

        [Fact]
        public async Task Delete_Ok()
        {
            // Arrange
            long id = 1;
            var updated = DateTime.UtcNow;

            // Act
            var result = await _roleDbService.Delete(id);

            // Assert
            Assert.IsType<RoleModel>(result.ProcessedObject);
            var model = result.ProcessedObject as RoleModel;
            Assert.True(model.IsDeleted);
            Assert.True(updated <= model.LastUpdated);
        }

        [Fact]
        public async Task Restore_Ok()
        {
            // Arrange
            long id = 3;
            var updated = DateTime.UtcNow;

            // Act
            var result = await _roleDbService.Restore(id);

            // Assert
            Assert.IsType<RoleModel>(result.ProcessedObject);
            var model = result.ProcessedObject as RoleModel;
            Assert.False(model.IsDeleted);
            Assert.True(updated <= model.LastUpdated);
        }

        [Theory]
        [MemberData(nameof(Data_NameExists))]
        public async Task CheckNameExists_True(string name)
        {
            // Arrange

            // Act
            var result = await _roleDbService.CheckNameExists(name);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [MemberData(nameof(Data_NameNotExists))]
        public async Task CheckNameExists_False(string name)
        {
            // Arrange

            // Act
            var result = await _roleDbService.CheckNameExists(name);

            // Assert
            Assert.False(result);
        }

        public static IEnumerable<object[]> Data_NameExists => new List<object[]>
        {
            new object[] { "admin" },
            new object[] { "Admin" },
        };

        public static IEnumerable<object[]> Data_NameNotExists => new List<object[]>
        {
            new object[] { "nothing" },
            new object[] { "" },
            new object[] { null },
        };
    }
}
