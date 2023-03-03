using AutoMapper;
using Microsoft.Extensions.Logging;
using SavaDev.Base.Data.Exceptions;
using SavaDev.Base.Data.Registry;
using SavaDev.Base.Data.Registry.Enums;
using SavaDev.Base.Data.Registry.Filter;
using SavaDev.Libs.UnitTestingHelpers;
using SavaDev.Services.Tests.System.Data;
using SavaDev.General.Data;
using SavaDev.General.Data.Contract.Models;
using SavaDev.General.Data.Services;

namespace SavaDev.Services.Tests.System.DataTests
{
    public class ReservedNameServiceTests : IDisposable
    {
        private IMapper _mapper;
        private GeneralContext _context;
        private ReservedNameService _reservedNameDbService;
        private ILogger<ReservedNameService> _logger;

        public ReservedNameServiceTests()
        {
            _mapper = Dependencies.GetDataMapper();
            _logger = TestsInfrastructure.GetLogger<ReservedNameService>();
            _context = TestsInfrastructure.GetContext<GeneralContext>(x => new GeneralContext(x));
            DataInit.FillContextWithEntities(_context);
            _reservedNameDbService = new ReservedNameService(_context, _mapper, _logger);
        }

        public void Dispose()
        {
            _mapper = null;
            _context = null;
            _reservedNameDbService = null;
            _logger = null;
        }

        [Theory]
        [MemberData(nameof(DataInit.GetReservedNamesTextInput), nameof(Create_Ok), MemberType = typeof(DataInit))]
        public async Task Create_Ok(string text)
        {
            // Arrange
            // TODO из файла читаются преобразованные значения без пробелов - посмотреть, где это обрабатывается. Должно быть 6 тест-кейсов, сейчас 2
            var rnModel = new ReservedNameModel() { Text = text, IncludePlural = true };

            // Act
            var result = await _reservedNameDbService.Create(rnModel);            

            // Assert
            Assert.IsType<ReservedNameModel>(result.ProcessedObject);
            var model = result.ProcessedObject as ReservedNameModel;
            Assert.Equal(text.ToLower().Trim(), model.Text);
        }

        [Theory]
        [MemberData(nameof(DataInit.GetReservedNamesTextInput), nameof(Create_NameExists), MemberType = typeof(DataInit))]
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
            await Assert.ThrowsAsync<ArgumentNullException>(action);
        }

        [Theory]
        [MemberData(nameof(CreateData_NameEmpty))]
        public async Task Create_NameEmpty(string name)
        {
            // Arrange
            var rnModel = new ReservedNameModel() { Text = name };

            // Act
            async Task action() => await _reservedNameDbService.Create(rnModel);

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(action);
        }

        [Theory]
        [MemberData(nameof(DataInit.GetReservedNamesTextInput), nameof(Create_NameWrong), MemberType = typeof(DataInit))]
        public async Task Create_NameWrong(string name)
        {
            // Arrange
            var rnModel = new ReservedNameModel() { Text = name };

            // Act
            async Task action() => await _reservedNameDbService.Create(rnModel);

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(action); // TODO здесь скорее всего нужна другая ошибка, но пока так
        }

        [Theory]
        [MemberData(nameof(DataInit.GetReservedNamesTextInput), nameof(Update_Ok), MemberType = typeof(DataInit))]
        public async Task Update_Ok(string text)
        {
            // Arrange
            var rnModel = new ReservedNameModel() { Text = text, IncludePlural = false };

            // Act
            var result = await _reservedNameDbService.Update(rnModel);

            //Assert
            Assert.IsType<ReservedNameModel>(result.ProcessedObject);
            var model = result.ProcessedObject as ReservedNameModel;
            Assert.Equal(rnModel.IncludePlural, model.IncludePlural);
        }

        [Theory]
        [MemberData(nameof(DataInit.GetReservedNamesTextInput), nameof(Update_EntityNotFound), MemberType = typeof(DataInit))]
        public async Task Update_EntityNotFound(string text)
        {
            // Arrange
            var rnModel = new ReservedNameModel() { Text = text, IncludePlural = false };

            // Act
            async Task action() => await _reservedNameDbService.Update(rnModel);

            // Assert
             await Assert.ThrowsAsync<EntityNotFoundException>(action);
        }

        [Theory]
        [MemberData(nameof(DataInit.GetReservedNamesTextInput), nameof(Remove_Ok), MemberType = typeof(DataInit))]
        public async Task Remove_Ok(string text)
        {
            // Arrange

            // Act
            await _reservedNameDbService.Remove(text);

            // Assert
            Assert.False(_context.ReservedNames.Any(m => m.Text == text));
        }

        [Theory]
        [MemberData(nameof(DataInit.GetReservedNamesTextInput), nameof(Remove_EntityNotFound), MemberType = typeof(DataInit))]
        public async Task Remove_EntityNotFound(string name)
        {
            // Arrange

            // Act
            async Task action() => await _reservedNameDbService.Remove(name);

            // Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(action);
        }

        [Theory]
        [MemberData(nameof(DataInit.GetReservedNamesTextInput), nameof(CheckIsReserved_True), MemberType = typeof(DataInit))]
        public async Task CheckIsReserved_True(string name)
        {
            // Arrange

            // Act
            var result = await _reservedNameDbService.CheckIsReserved(name);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [MemberData(nameof(DataInit.GetReservedNamesTextInput), nameof(CheckIsReserved_False), MemberType = typeof(DataInit))]
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
        [MemberData(nameof(DataInit.GetReservedNamesTextInput), nameof(CheckExists_True), MemberType = typeof(DataInit))]
        public async Task CheckExists_True(string name)
        {
            // Arrange

            // Act
            var result = await _reservedNameDbService.CheckExists(name);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [MemberData(nameof(DataInit.GetReservedNamesTextInput), nameof(CheckExists_False), MemberType = typeof(DataInit))]
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
        [MemberData(nameof(DataInit.GetReservedNamesTextInput), nameof(GetOne_Ok), MemberType = typeof(DataInit))]
        public async Task GetOne_Ok(string name)
        {
            // Arrange

            // Act
            var result = await _reservedNameDbService.GetOne(name);

            // Assert
            Assert.Equal(name, result.Text);
        }

        [Theory]
        [MemberData(nameof(DataInit.GetReservedNamesTextInput), nameof(GetOne_NotFound), MemberType = typeof(DataInit))]
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
        public async Task GetRegistryPage_ByName_CheckNames(MatchModeWord matchMode, List<string> values, string[] expectedLogins)
        {
            // Arrange
            var query = new RegistryQuery<ReservedNameFilterModel>(new RegistryPageInfo(), new RegistrySort("Text"))
            {
                Filter0 = new ReservedNameFilterModel
                {
                    Text = new WordFilterField
                    {
                        MatchMode = matchMode,
                        Value = values
                    }
                }
            };

            // Act
            var result = await _reservedNameDbService.GetRegistryPage(query);

            // Assert
            Assert.Equal(expectedLogins, result.Items.Select(m => m.Text).ToArray());
        }

        #region Test Cases

        public static IEnumerable<object[]> CreateData_NameEmpty => new List<object[]>
        {
            new object[] { string.Empty },
            new object[] { "" },
            new object[] { " " },
            new object[] { "  " },
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
