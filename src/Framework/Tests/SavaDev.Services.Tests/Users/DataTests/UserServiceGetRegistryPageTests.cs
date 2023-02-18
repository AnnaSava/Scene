using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SavaDev.Base.Data.Registry;
using SavaDev.Base.Data.Registry.Enums;
using SavaDev.Libs.UnitTestingHelpers;
using SavaDev.Services.Tests.Users;
using SavaDev.Services.Tests.Users.DataTests.Data;
using SavaDev.Users.Data;
using SavaDev.Users.Data.Contract.Models;
using SavaDev.Users.Data.Entities;

namespace SavaDev.Services.Tests.Users.DataTests
{
    public class UserServiceGetRegistryPageTests : IDisposable
    {
        private IMapper _mapper;
        private UsersContext _context;
        private UserManager<User> _userManager;
        private UserService _userDbService;
        private ILogger<UserService> _logger;

        public UserServiceGetRegistryPageTests()
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

        //[Theory]
        //[MemberData(nameof(FilterData_CheckCount))]
        //public async Task GetAll_CheckCount(RegistryQuery<UserFilterModel> query, long expectedCount)
        //{
        //    // Arrange

        //    // Act
        //    var result = await _userDbService.GetAll(query);

        //    // Assert
        //    Assert.Equal(expectedCount, result.TotalRows);
        //}

        //[Theory]
        //[MemberData(nameof(FilterData_Ids))]
        //[MemberData(nameof(FilterData_Ids_Ext))]
        //public async Task GetAll_ById_CheckIds(MatchModeNumeric matchMode, List<long> values, long[] expectedIds)
        //{
        //    // Arrange
        //    var query = new RegistryQuery<UserFilterModel>
        //    {
        //        Filter = new UserFilterModel
        //        {
        //            Ids = new NumericFilterField<long>
        //            {
        //                MatchMode = matchMode,
        //                Value = values
        //            }
        //        }
        //    };

        //    // Act
        //    var result = await _userDbService.GetAll(query);

        //    // Assert
        //    Assert.Equal(expectedIds, result.Items.Select(m => m.Id).ToArray());
        //}

        //[Theory]
        //[MemberData(nameof(FilterData_Ids_NotFound))]
        //public async Task GetAll_ById_NotFound(MatchModeNumeric matchMode, List<long> values)
        //{
        //    // Arrange
        //    var query = new RegistryQuery<UserFilterModel>
        //    {
        //        Filter = new UserFilterModel
        //        {
        //            Ids = new NumericFilterField<long>
        //            {
        //                MatchMode = matchMode,
        //                Value = values
        //            }
        //        }
        //    };

        //    // Act
        //    var result = await _userDbService.GetAll(query);

        //    // Assert
        //    Assert.Equal(0, result.TotalRows);
        //}

        //[Theory]
        //[MemberData(nameof(FilterData_Logins))]
        //public async Task GetAll_ByLogin_CheckLogins(MatchModeWord matchMode, List<string> values, string[] expectedLogins)
        //{
        //    // Arrange
        //    var query = new RegistryQuery<UserFilterModel>
        //    {
        //        Filter = new UserFilterModel
        //        {
        //            Logins = new WordFilterField
        //            {
        //                MatchMode = matchMode,
        //                Value = values
        //            }
        //        }
        //    };

        //    // Act
        //    var result = await _userDbService.GetAll(query);

        //    // Assert
        //    Assert.Equal(expectedLogins, result.Items.Select(m => m.Login).ToArray());
        //}

        //[Theory]
        //[MemberData(nameof(SortData_Fields_CheckFirstId))]
        //public async Task GetAll_Sort_CheckFirstId(string fieldName, SortDirection direction, long expectedFirstId)
        //{
        //    // Arrange
        //    var query = new RegistryQuery<UserFilterModel>
        //    {
        //        PageInfo = new RegistryPageInfo
        //        {
        //            Sort = new List<ListSortModel>
        //                {
        //                    new ListSortModel { FieldName = fieldName, Direction = direction}
        //                }
        //        }
        //    };

        //    // Act
        //    var result = await _userDbService.GetAll(query);

        //    // Assert
        //    Assert.Equal(expectedFirstId, result.Items.First().Id);
        //}

        //[Theory]
        //[MemberData(nameof(SortData_Paging_CheckFirstId))]
        //public async Task GetAll_SortIdAsc_Paging_CheckFirstId(int pageNumber, int rowsCount, long expectedFirstId)
        //{
        //    // Arrange
        //    var query = new RegistryQuery<UserFilterModel>
        //    {
        //        PageInfo = new RegistryPageInfo
        //        {
        //            PageNumber = pageNumber,
        //            RowsCount = rowsCount,
        //            Sort = new List<ListSortModel>
        //            {
        //                new ListSortModel { FieldName = "Id", Direction = SortDirection.Asc }
        //            }
        //        }
        //    };

        //    // Act
        //    var result = await _userDbService.GetAll(query);

        //    // Assert
        //    Assert.Equal(expectedFirstId, result.Items.First().Id);
        //    Assert.Equal(pageNumber, result.Page);
        //}

        //[Theory]
        //[MemberData(nameof(SortData_Paging_CheckFirstId))]
        //public async Task GetAll_SortDefault_Paging_CheckFirstId(int pageNumber, int rowsCount, long expectedFirstId)
        //{
        //    // Arrange
        //    var query = new RegistryQuery<UserFilterModel>
        //    {
        //        PageInfo = new RegistryPageInfo
        //        {
        //            PageNumber = pageNumber,
        //            RowsCount = rowsCount,
        //        }
        //    };

        //    // Act
        //    var result = await _userDbService.GetAll(query);

        //    // Assert
        //    Assert.Equal(expectedFirstId, result.Items.First().Id);
        //    Assert.Equal(pageNumber, result.Page);
        //}

        #region Test Cases

        public static IEnumerable<object[]> FilterData_CheckCount => new List<object[]>
        {
            new object[] { new RegistryQuery<UserFilterModel> (), 8 },
            new object[] { new RegistryQuery<UserFilterModel> { Filter = new UserFilterModel () }, 8 },
            new object[] { new RegistryQuery<UserFilterModel> { Filter = new UserFilterModel { IsDeleted = true } }, 2 },
            new object[] { new RegistryQuery<UserFilterModel> { Filter = new UserFilterModel { IsDeleted = false } }, 8 },
            //new object[] { new RegistryQuery<UserFilterModel> { Filter = new UserFilterModel { IsBanned = true } }, 3 },
            //new object[] { new RegistryQuery<UserFilterModel> { Filter = new UserFilterModel { IsBanned = false } }, 5 },
        };

        public static IEnumerable<object[]> FilterData_Ids => new List<object[]>
        {
            new object[] { MatchModeNumeric.Equals, new List<long> { 1 }, new long[] { 1 } },
            new object[] { MatchModeNumeric.NotEquals, new List<long> { 1 }, new long[] { 2, 3, 4, 5, 6, 7, 8 } },
            new object[] { MatchModeNumeric.In, new List<long> { 1, 3, 5 }, new long[] { 1, 3, 5 } },
            new object[] { MatchModeNumeric.NotIn, new List<long> { 1, 3, 5 }, new long[] { 2, 4, 6, 7, 8 } },
            new object[] { MatchModeNumeric.Lt, new List<long> { 3 }, new long[] { 1, 2 } },
            new object[] { MatchModeNumeric.Gt, new List<long> { 6 }, new long[] { 7, 8 } },
            new object[] { MatchModeNumeric.Between, new List<long> { 5, 8 }, new long[] { 5, 6, 7, 8 } },
        };

        public static IEnumerable<object[]> FilterData_Ids_Ext => new List<object[]>
        {
            new object[] { MatchModeNumeric.Equals, new List<long> { 1, 2 }, new long[] { 1 } },
            new object[] { MatchModeNumeric.Between, new List<long> { 5, 2 }, Array.Empty<long>() },
            new object[] { MatchModeNumeric.Between, new List<long> { 2, 5, 7 }, new long[] { 2, 3, 4, 5, 6, 7 } },
            new object[] { MatchModeNumeric.Gt, new List<long> { 5, 7 }, new long[] { 6, 7, 8 } },
            new object[] { MatchModeNumeric.Gt, new List<long> { 7, 5 }, new long[] { 8 } },
            new object[] { MatchModeNumeric.Equals, null, new long[] { 1, 2, 3, 4, 5, 6, 7, 8 } },
            new object[] { MatchModeNumeric.Equals, new List<long> { }, new long[] { 1, 2, 3, 4, 5, 6, 7, 8 } },
        };

        public static IEnumerable<object[]> FilterData_Ids_NotFound => new List<object[]>
        {
            new object[] { MatchModeNumeric.Equals, new List<long> { 10 } },
            new object[] { MatchModeNumeric.Equals, new List<long> { 0 } },
            new object[] { MatchModeNumeric.Equals, new List<long> { 11 } },
            new object[] { MatchModeNumeric.Equals, new List<long> { -1 } },
            new object[] { MatchModeNumeric.Lt, new List<long> { 1 } },
            new object[] { MatchModeNumeric.Gt, new List<long> { 8 } },
            new object[] { MatchModeNumeric.Between, new List<long> { 9, 11 } },
        };

        public static IEnumerable<object[]> FilterData_Logins => new List<object[]>
        {
            new object[] { MatchModeWord.Equals, new List<string> { "adm" }, new string[] { "adm" } },
            new object[] { MatchModeWord.NotEquals, new List<string> { "adm" }, new string[] { "qwe", "asd", "zxc", "qaz", "wsx", "edc", "rfv" } },
            new object[] { MatchModeWord.In, new List<string> { "adm", "qwe", "asd", "zxc" }, new string[] { "adm", "qwe", "asd", "zxc" } },
            new object[] { MatchModeWord.NotIn, new List<string> { "adm", "qwe", "asd", "zxc" }, new string[] { "qaz", "wsx", "edc", "rfv" } },
            new object[] { MatchModeWord.StartsWith, new List<string> { "a" }, new string[] { "adm", "asd" } },
            new object[] { MatchModeWord.EndsWith, new List<string> { "c" }, new string[] { "zxc", "edc" } },
            new object[] { MatchModeWord.Contains, new List<string> { "s" }, new string[] { "asd", "wsx" } },
            new object[] { MatchModeWord.NotContains, new List<string> { "a" }, new string[] { "qwe", "zxc", "wsx", "edc", "rfv" } },
        };

        public static IEnumerable<object[]> SortData_Fields_CheckFirstId => new List<object[]>
        {
            new object[] { "Id", SortDirection.Asc, 1 },
            new object[] { "Id", SortDirection.Desc, 8 },
            new object[] { "UserName", SortDirection.Asc, 1 },
            new object[] { "UserName", SortDirection.Desc, 4 },
            new object[] { "Email", SortDirection.Asc, 1 },
            new object[] { "Email", SortDirection.Desc, 4 },
            new object[] { "PhoneNumber", SortDirection.Asc, 1 },
            new object[] { "PhoneNumber", SortDirection.Desc, 8 },
            new object[] { "FirstName", SortDirection.Asc, 1 },
            new object[] { "FirstName", SortDirection.Desc, 4 },
            new object[] { "LastName", SortDirection.Asc, 1 },
            new object[] { "LastName", SortDirection.Desc, 4 },
            new object[] { "DisplayName", SortDirection.Asc, 1 },
            new object[] { "DisplayName", SortDirection.Desc, 4 },
        };

        public static IEnumerable<object[]> SortData_Paging_CheckFirstId => new List<object[]>
        {
            new object[] { 2, 3, 4 },
        };

        #endregion
    }
}
