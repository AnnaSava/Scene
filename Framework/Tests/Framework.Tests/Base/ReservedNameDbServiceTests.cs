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
    public class ReservedNameDbServiceTests : IDisposable
    {
        private IMapper _mapper;
        private ReservedNameTestDbContext _context;
        private ReservedNameDbService _reservedNameDbService;

        public ReservedNameDbServiceTests()
        {
            _mapper = new MapperConfiguration(opts => { opts.AddProfile<ReservedNameDataMapperProfile>(); }).CreateMapper();
            _context = GetContext();
            _reservedNameDbService = new ReservedNameDbService(_context, _mapper);
            FillContextWithTestData(_context, TestData.GetReservedNames());
        }

        public void Dispose()
        {
            _mapper = null;
            _context = null;
            _reservedNameDbService = null;
        }

        [Theory]
        [MemberData(nameof(CreateData_NameOk))]
        public async Task Create_Ok(string text, string expectedText)
        {
            // Arrange
            var rnModel = new ReservedNameModel() { Text = text, IncludePlural = true };

            // Act
            var model = await _reservedNameDbService.Create(rnModel);

            // Assert
            Assert.Equal(expectedText, model.Text);
        }

        [Theory]
        [MemberData(nameof(Data_NameExists))]
        public async Task Create_NameExists(string text)
        {
            // Arrange
            var rnModel = new ReservedNameModel() { Text = text };

            // Act
            async Task action() => await _reservedNameDbService.Create(rnModel);

            // Assert
            await Assert.ThrowsAsync<InvalidOperationException>(action);
        }

        [Fact]
        public async Task Create_NameNull()
        {
            // Arrange
            var rnModel = new ReservedNameModel() { Text = null };

            // Act
            async Task action() => await _reservedNameDbService.Create(rnModel);

            // Assert
            await Assert.ThrowsAsync<InvalidOperationException>(action);
        }

        [Theory]
        [MemberData(nameof(CreateData_NameEmpty))]
        [MemberData(nameof(CreateData_NameWrong))]
        public async Task Create_NameWrong(string name)
        {
            // Arrange
            var rnModel = new ReservedNameModel() { Text = name };

            // Act
            async Task action() => await _reservedNameDbService.Create(rnModel);

            // Assert
            await Assert.ThrowsAsync<InvalidOperationException>(action);
        }

        [Theory]
        [MemberData(nameof(Data_NameExists))]
        public async Task Update_Ok(string text)
        {
            // Arrange
            var rnModel = new ReservedNameModel() { Text = text, IncludePlural = false };

            // Act
            var model = await _reservedNameDbService.Update(rnModel);

            // Assert
            Assert.Equal(rnModel.IncludePlural, model.IncludePlural);
        }

        [Fact]
        public async Task Update_EntityNotFound()
        {
            // Arrange
            var rnModel = new ReservedNameModel() { Text = "god", IncludePlural = false };

            // Act
            async Task action() => await _reservedNameDbService.Update(rnModel);

            // Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(action);
        }

        [Theory]
        [MemberData(nameof(Data_NameExists))]
        public async Task Remove_Ok(string text)
        {
            // Arrange

            // Act
            await _reservedNameDbService.Remove(text);

            // Assert
            Assert.False(_context.ReservedNames.Any(m => m.Text == text));
        }

        [Theory]
        [MemberData(nameof(Data_NameNotExists))]
        public async Task Remove_EntityNotFound(string name)
        {
            // Arrange

            // Act
            async Task action() => await _reservedNameDbService.Remove(name); 

            // Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(action);
        }

        [Theory]
        [MemberData(nameof(CheckReservedData_Names))]
        public async Task CheckIsReserved_True(string name)
        {
            // Arrange

            // Act
            var result = await _reservedNameDbService.CheckIsReserved(name);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [MemberData(nameof(CheckReservedData_Names_NotReserved))]
        public async Task CheckIsReserved_False(string name)
        {
            // Arrange

            // Act
            var result = await _reservedNameDbService.CheckIsReserved(name);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task CheckIsReserved_Null()
        {
            // Arrange
            string name = null;

            // Act
            async Task action() => await _reservedNameDbService.CheckIsReserved(name);

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(action);
        }

        [Theory]
        [MemberData(nameof(Data_NameExists))]
        public async Task CheckExists_True(string name)
        {
            // Arrange

            // Act
            var result = await _reservedNameDbService.CheckExists(name);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [MemberData(nameof(Data_NameNotExists))]
        public async Task CheckExists_False(string name)
        {
            // Arrange

            // Act
            var result = await _reservedNameDbService.CheckExists(name);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task CheckExists_Null()
        {
            // Arrange
            string name = null;

            // Act
            async Task action() => await _reservedNameDbService.CheckExists(name);

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(action);
        }

        [Theory]
        [MemberData(nameof(GetData_NameExists))]
        public async Task GetOne_Ok(string name, string expectedName)
        {
            // Arrange

            // Act
            var result = await _reservedNameDbService.GetOne(name);

            // Assert
            Assert.Equal(expectedName, result.Text);
        }

        [Theory]
        [MemberData(nameof(Data_NameNotExists))]
        public async Task GetOne_NotFound(string name)
        {
            // Arrange

            // Act
            var result = await _reservedNameDbService.GetOne(name);

            // Assert
            Assert.Null(result);
        }

        [Theory]
        [MemberData(nameof(FilterData_Names))]
        public async Task GetAll_ByName_CheckNames(MatchModeWord matchMode, List<string> values, string[] expectedLogins)
        {
            // Arrange
            var query = new ListQueryModel<ReservedNameFilterModel>
            {
                Filter = new ReservedNameFilterModel
                {
                    Text = new WordFilterField
                    {
                        MatchMode = matchMode,
                        Value = values
                    }
                }
            };

            // Act
            var result = await _reservedNameDbService.GetAll(query);

            // Assert
            Assert.Equal(expectedLogins, result.Items.Select(m => m.Text).ToArray());
        }

        #region Init

        private ReservedNameTestDbContext GetContext()
        {
            var options = GetOptionsAction();

            var optionsBuilder = new DbContextOptionsBuilder<ReservedNameTestDbContext>();
            options.Invoke(optionsBuilder);

            return new ReservedNameTestDbContext(optionsBuilder.Options);
        }
        private Action<DbContextOptionsBuilder> GetOptionsAction() => options => options.UseInMemoryDatabase(Guid.NewGuid().ToString());

        private void FillContextWithTestData(ReservedNameTestDbContext context, IEnumerable<ReservedName> data)
        {
            context.Database.EnsureCreated();
            context.ReservedNames.AddRange(data);
            context.SaveChanges();
        }

        #endregion

        #region Test Cases

        public static IEnumerable<object[]> CreateData_NameOk => new List<object[]>
        {
            new object[] { "system", "system" },
            new object[] { "system2", "system2" },
            new object[] { "System", "system" },
            new object[] { "system ", "system" },
            new object[] { " system", "system" },
            new object[] { " system ", "system" },
        };

        public static IEnumerable<object[]> Data_NameExists => new List<object[]>
        {
            new object[] { "admin" },
            new object[] { "Admin" },
        };

        public static IEnumerable<object[]> GetData_NameExists => new List<object[]>
        {
            new object[] { "admin", "admin" },
            new object[] { "Admin", "admin" },
        };

        public static IEnumerable<object[]> Data_NameNotExists => new List<object[]>
        {
            new object[] { "god" },
            new object[] { "admins" },
        };

        public static IEnumerable<object[]> CreateData_NameEmpty => new List<object[]>
        {
            new object[] { string.Empty },
            new object[] { "" },
            new object[] { " " },
            new object[] { "  " },
        };

        public static IEnumerable<object[]> CreateData_NameWrong => new List<object[]>
        {
            new object[] { "sp ace" },
            new object[] { "pl+s" },
        };

        public static IEnumerable<object[]> CheckReservedData_Names => new List<object[]>
        {
            new object[] { "admin" },
            new object[] { "admins" },
            new object[] { "create" },
            new object[] { "Admin" },
        };

        public static IEnumerable<object[]> CheckReservedData_Names_NotReserved => new List<object[]>
        {
            new object[] { "creates" },
            new object[] { "god" },
            new object[] { "" },
        };

        public static IEnumerable<object[]> FilterData_Names => new List<object[]>
        {
            new object[] { MatchModeWord.Equals, new List<string> { "admin" }, new string[] { "admin" } },
            new object[] { MatchModeWord.NotEquals, new List<string> { "admin" }, new string[] { "create", "drop", "moderator" } },
            new object[] { MatchModeWord.In, new List<string> { "admin", "drop" }, new string[] { "admin", "drop" } },
            new object[] { MatchModeWord.NotIn, new List<string> { "admin", "drop" }, new string[] { "create", "moderator" } },
            new object[] { MatchModeWord.StartsWith, new List<string> { "a" }, new string[] { "admin" } },
            new object[] { MatchModeWord.EndsWith, new List<string> { "p" }, new string[] { "drop" } },
            new object[] { MatchModeWord.Contains, new List<string> { "o" }, new string[] { "drop", "moderator" } },
            new object[] { MatchModeWord.NotContains, new List<string> { "e" }, new string[] { "admin", "drop" } },
        };

        #endregion
    }
}
