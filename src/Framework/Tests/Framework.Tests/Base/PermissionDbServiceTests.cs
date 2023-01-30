using AutoMapper;
using Framework.Base.DataService.Contract.Models;
using Framework.Base.DataService.Exceptions;
using Framework.Base.Types.Enums;
using Framework.Tests.Base.Data;
using Framework.User.DataService.Contract.Models;
using Framework.User.DataService.Entities;
using Framework.User.DataService.Mapper;
using Framework.User.DataService.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Framework.Tests.Base
{
    public class PermissionDbServiceTests : IDisposable
    {
        private IMapper _mapper;
        private PermissionTestDbContext _context;
        private PermissionDbService _permissionDbService;

        public PermissionDbServiceTests()
        {
            _mapper = new MapperConfiguration(opts => { opts.AddProfile<CommonDataMapperProfile>(); }).CreateMapper();
            _context = GetContext();

            // TODO logger
            _permissionDbService = new PermissionDbService(_context, _mapper, null);
            FillContextWithTestData(_context, TestData.GetPermissions());
        }

        public void Dispose()
        {
            _mapper = null;
            _context = null;
            _permissionDbService = null;
        }

        [Theory]
        [MemberData(nameof(Data_NameExists))]
        public async Task CheckExists_True(string name)
        {
            // Arrange

            // Act
            var result = await _permissionDbService.CheckExists(name);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [MemberData(nameof(Data_NameNotExists))]
        public async Task CheckExists_False(string name)
        {
            // Arrange

            // Act
            var result = await _permissionDbService.CheckExists(name);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task CheckExists_Null()
        {
            // Arrange
            string name = null;

            // Act
            async Task action() => await _permissionDbService.CheckExists(name);

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(action);
        }

        [Theory]
        [MemberData(nameof(GetData_NameExists))]
        public async Task GetOne_Ok(string name, string expectedName)
        {
            // Arrange

            // Act
            var result = await _permissionDbService.GetOne(name);

            // Assert
            Assert.Equal(expectedName, result.Name);
        }

        [Theory]
        [MemberData(nameof(Data_NameNotExists))]
        public async Task GetOne_NotFound(string name)
        {
            // Arrange

            // Act
            var result = await _permissionDbService.GetOne(name);

            // Assert
            Assert.Null(result);
        }

        [Theory]
        [MemberData(nameof(FilterData_Names))]
        public async Task GetAll_ByName_CheckNames(MatchModeWord matchMode, List<string> values, string[] expectedNames)
        {
            // Arrange
            var query = new ListQueryModel<PermissionFilterModel>
            {
                Filter = new PermissionFilterModel
                {
                    Name = new WordFilterField
                    {
                        MatchMode = matchMode,
                        Value = values
                    }
                }
            };

            // Act
            var result = await _permissionDbService.GetAll(query);

            // Assert
            Assert.Equal(expectedNames, result.Items.Select(m => m.Name).ToArray());
        }

        [Fact]
        public async Task FilterExisting_Ok()
        {
            // Arrange
            var names = new string[] { "user.create", "user.Update", "user.sendtospace" };

            // Act
            var result = await _permissionDbService.FilterExisting(names);

            // Assert
            Assert.Equal(new string[] { "user.create", "user.update" }, result);
        }

        [Fact]
        public async Task FilterExisting_Distinct_Ok()
        {
            // Arrange
            var names = new string[] { "user.create", "user.create", "user.Update", "user.sendtospace" };

            // Act
            var result = await _permissionDbService.FilterExisting(names);

            // Assert
            Assert.Equal(new string[] { "user.create", "user.update" }, result);
        }

        [Fact]
        public async Task FilterExisting_Null()
        {
            // Arrange
            string[] names = null;

            // Act
            var result = await _permissionDbService.FilterExisting(names);

            // Assert
            Assert.Null(result);
        }

        #region Init

        private PermissionTestDbContext GetContext()
        {
            var options = GetOptionsAction();

            var optionsBuilder = new DbContextOptionsBuilder<PermissionTestDbContext>();
            options.Invoke(optionsBuilder);

            return new PermissionTestDbContext(optionsBuilder.Options);
        }
        private Action<DbContextOptionsBuilder> GetOptionsAction() => options => options.UseInMemoryDatabase(Guid.NewGuid().ToString());

        private void FillContextWithTestData(PermissionTestDbContext context, IEnumerable<Permission> data)
        {
            context.Database.EnsureCreated();
            context.Permissions.AddRange(data);
            context.SaveChanges();
        }

        #endregion

        #region Test Cases

        public static IEnumerable<object[]> GetData_NameExists => new List<object[]>
        {
            new object[] { "user.create", "user.create" },
            new object[] { "User.Create", "user.create" },
        };

        public static IEnumerable<object[]> Data_NameExists => new List<object[]>
        {
            new object[] { "user.create" },
            new object[] { "User.Create" },
        };

        public static IEnumerable<object[]> Data_NameNotExists => new List<object[]>
        {
            new object[] { "user.sendtospace" }
        };

        public static IEnumerable<object[]> FilterData_Names => new List<object[]>
        {
            new object[] { MatchModeWord.Equals, new List<string> { "user.create" }, new string[] { "user.create" } },
            new object[] { MatchModeWord.NotEquals, new List<string> { "user.create" }, new string[] { "user.delete", "user.update" } },
            new object[] { MatchModeWord.In, new List<string> { "user.create", "user.update" }, new string[] { "user.create", "user.update" } },
            new object[] { MatchModeWord.NotIn, new List<string> { "user.create", "user.update" }, new string[] { "user.delete" } },
            new object[] { MatchModeWord.StartsWith, new List<string> { "user.c" }, new string[] { "user.create" } },
            new object[] { MatchModeWord.EndsWith, new List<string> { "lete" }, new string[] { "user.delete" } },
            new object[] { MatchModeWord.Contains, new List<string> { "at" }, new string[] { "user.create", "user.update" } },
            new object[] { MatchModeWord.NotContains, new List<string> { "at" }, new string[] { "user.delete" } },
        };

        #endregion
    }
}
