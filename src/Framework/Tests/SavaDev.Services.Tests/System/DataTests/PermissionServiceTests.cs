using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SavaDev.System.Data.Entities;
using SavaDev.System.Data.Services;
using SavaDev.System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SavaDev.Libs.UnitTestingHelpers;
using SavaDev.Services.Tests.System.Data;
using SavaDev.System.Data.Contract;
using Microsoft.Extensions.Logging;
using SavaDev.Base.Data.Registry.Enums;
using SavaDev.Base.Data.Registry;
using SavaDev.System.Data.Contract.Models;
using SavaDev.Base.Data.Registry.Filter;

namespace SavaDev.Services.Tests.System.DataTests
{
    public class PermissionServiceTests : IDisposable
    {
        private IMapper _mapper;
        private SystemContext _context;
        private PermissionService _permissionDbService;
        private ILogger<PermissionService> _logger;

        public PermissionServiceTests()
        {
            _mapper = Dependencies.GetDataMapper();
            _logger = TestsInfrastructure.GetLogger<PermissionService>();
            _context = TestsInfrastructure.GetContext<SystemContext>(x => new SystemContext(x));
            DataInit.FillContextWithEntities(_context);
            _permissionDbService = new PermissionService(_context, _mapper, _logger);
        }

        public void Dispose()
        {
            _mapper = null;
            _context = null;
            _permissionDbService = null;
            _logger = null;
        }

        #region CheckExists

        [Theory]
        [MemberData(nameof(DataInit.GetPermissionNamesInput), nameof(CheckExists_True), MemberType = typeof(DataInit))]
        public async Task CheckExists_True(string name)
        {
            // Arrange

            // Act
            var result = await _permissionDbService.CheckExists(name);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [MemberData(nameof(DataInit.GetPermissionNamesInput), nameof(CheckExists_False), MemberType = typeof(DataInit))]
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

        #endregion

        #region GetOne

        [Theory]
        [MemberData(nameof(DataInit.GetPermissionNamesInput), nameof(GetOne_Ok), MemberType = typeof(DataInit))]
        public async Task GetOne_Ok(string name)
        {
            // Arrange

            // Act
            var result = await _permissionDbService.GetOne(name);

            // Assert
            Assert.Equal(name.ToLower(), result.Name);
        }

        [Theory]
        [MemberData(nameof(DataInit.GetPermissionNamesInput), nameof(GetOne_NotFound), MemberType = typeof(DataInit))]
        public async Task GetOne_NotFound(string name)
        {
            // Arrange

            // Act
            var result = await _permissionDbService.GetOne(name);

            // Assert
            Assert.Null(result);
        }

        #endregion

        [Theory]
        [MemberData(nameof(FilterData_Names))]
        public async Task GeRegistryPage_ByName_CheckNames(MatchModeWord matchMode, List<string> values, string[] expectedNames)
        {
            // Arrange
            var query = new RegistryQuery<PermissionFilterModel>(new RegistryPageInfo(), new RegistrySort("Name"))
            {
                Filter0 = new PermissionFilterModel
                {
                    Name = new WordFilterField
                    {
                        MatchMode = matchMode,
                        Value = values
                    }
                }
            };

            // Act
            var result = await _permissionDbService.GetRegistryPage(query);

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

        #region Test Cases

        public static IEnumerable<object[]> FilterData_Names => new List<object[]>
        {
            new object[] { MatchModeWord.Equals, new List<string> { "user.create" }, new string[] { "user.create" } },
            //new object[] { MatchModeWord.NotEquals, new List<string> { "user.create" }, new string[] { "user.delete", "user.update" } },
            //new object[] { MatchModeWord.In, new List<string> { "user.create", "user.update" }, new string[] { "user.create", "user.update" } },
            //new object[] { MatchModeWord.NotIn, new List<string> { "user.create", "user.update" }, new string[] { "user.delete" } },
            //new object[] { MatchModeWord.StartsWith, new List<string> { "user.c" }, new string[] { "user.create" } },
            //new object[] { MatchModeWord.EndsWith, new List<string> { "lete" }, new string[] { "user.delete" } },
            //new object[] { MatchModeWord.Contains, new List<string> { "at" }, new string[] { "user.create", "user.update" } },
            //new object[] { MatchModeWord.NotContains, new List<string> { "at" }, new string[] { "user.delete" } },
        };

        #endregion
    }
}