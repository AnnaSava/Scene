using AutoMapper;
using Microsoft.Extensions.Logging;
using SavaDev.Base.Data.Managers.Crud;
using SavaDev.Base.Data.Registry;
using SavaDev.Base.Data.Registry.Enums;
using SavaDev.Base.Data.Registry.Filter;
using SavaDev.Libs.Tests.Base.Data.Managers.Fake;
using SavaDev.Libs.UnitTestingHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Libs.Tests.Base.Data.Managers
{
    public class GetRegistryTests
    {
        private IMapper _mapper;
        private ILogger<GetRegistryTests> _logger;
        private TestDbContext _context;
        private EntitySelector<long, TestEntity, TestModel, TestFilterModel> selector; 

        public GetRegistryTests()
        {
            _mapper = Dependencies.GetDataMapper();
            _logger = TestsInfrastructure.GetLogger<GetRegistryTests>();
            _context = TestsInfrastructure.GetContext<TestDbContext>(x => new TestDbContext(x));
            DataInit.FillContextWithEntities(_context);
            //_legalDocumentService = new LegalDocumentService(_context, _cultures, _mapper, _logger);
            selector = new EntitySelector<long, TestEntity, TestModel, TestFilterModel>(_context, _mapper, _logger);
        }


        [Theory]
        [MemberData(nameof(FilterData_CheckCount))]
        public async Task GetAll_CheckCount(RegistryQuery<TestFilterModel> query, long expectedCount)
        {
            // Arrange

            // Act
            var result = await selector.Query(query).ToRegistryPage();

            // Assert
            Assert.Equal(expectedCount, result.TotalRows);
        }

        [Theory]
        [MemberData(nameof(FilterData_Ids))]
        [MemberData(nameof(FilterData_Ids_Ext))]
        public async Task GetAll_ById_CheckIds(MatchModeNumeric matchMode, List<long> values, long[] expectedIds)
        {
            // Arrange
            var query = new RegistryQuery<TestFilterModel>
            {
                Filter0 = new TestFilterModel
                {
                    Id = new NumericFilterField<long>
                    {
                        MatchMode = matchMode,
                        Value = values
                    }
                }
            };

            // Act
            var result = await selector.Query(query).ToRegistryPage();

            // Assert
            Assert.Equal(expectedIds, result.Items.Select(m => m.Id).ToArray());
        }

        [Theory]
        [MemberData(nameof(FilterData_Ids_NotFound))]
        public async Task GetAll_ById_NotFound(MatchModeNumeric matchMode, List<long> values)
        {
            // Arrange
            var query = new RegistryQuery<TestFilterModel>
            {
                Filter = new TestFilterModel
                {
                    Id = new NumericFilterField<long>
                    {
                        MatchMode = matchMode,
                        Value = values
                    }
                }
            };

            // Act
            var result = await selector.Query(query).ToRegistryPage();

            // Assert
            Assert.Equal(0, result.TotalRows);
        }

        [Fact]
        public async Task GetAll_ById_MatchModeNotSet()
        {
            // Arrange
            var query = new RegistryQuery<TestFilterModel>
            {
                Filter = new TestFilterModel
                {
                    Id = new NumericFilterField<long>()
                }
            };
            var expectedIds = new long[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            // Act
            var result = await selector.Query(query).ToRegistryPage();

            // Assert
            Assert.Equal(expectedIds, result.Items.Select(m => m.Id).ToArray());
        }

        [Theory]
        [MemberData(nameof(SortData_Fields_CheckFirstId))]
        public async Task GetAll_Sort_CheckFirstId(string fieldName, SortDirection direction, long expectedFirstId)
        {
            // Arrange
            var query = new RegistryQuery<TestFilterModel>()
            {
                Sort = new List<RegistrySort>()
                {
                    new RegistrySort(fieldName,direction)
                }
            };

            // Act
            var result = await selector.Query(query).ToRegistryPage();

            // Assert
            Assert.Equal(expectedFirstId, result.Items.First().Id);
        }

        [Theory]
        [MemberData(nameof(SortData_Paging_CheckFirstId))]
        public async Task GetAll_SortIdAsc_Paging_CheckFirstId(int pageNumber, int rowsCount, long expectedFirstId)
        {
            // Arrange
            var query = new RegistryQuery<TestFilterModel>
            {
                PageInfo = new RegistryPageInfo
                {
                    PageNumber = pageNumber,
                    RowsCount = rowsCount,

                },
                Sort = new List<RegistrySort> { new RegistrySort("Id", SortDirection.Asc) }
            };

            // Act
            var result = await selector.Query(query).ToRegistryPage();

            // Assert
            Assert.Equal(expectedFirstId, result.Items.First().Id);
            Assert.Equal(pageNumber, result.Page);
        }

        [Theory]
        [MemberData(nameof(SortData_Paging_CheckFirstId))]
        public async Task GetAll_SortDefault_Paging_CheckFirstId(int pageNumber, int rowsCount, long expectedFirstId)
        {
            // Arrange
            var query = new RegistryQuery<TestFilterModel>
            {
                PageInfo = new RegistryPageInfo
                {
                    PageNumber = pageNumber,
                    RowsCount = rowsCount,
                }
            };

            // Act
            var result = await selector.Query(query).ToRegistryPage();

            // Assert
            Assert.Equal(expectedFirstId, result.Items.First().Id);
            Assert.Equal(pageNumber, result.Page);
        }

        // TODO проработать кейс
        //[Fact]
        private async Task GetAll_Sort_WrongField_CheckFirstId()
        {
            // Arrange
            var query = new RegistryQuery<TestFilterModel>
            {
                Sort = new List<RegistrySort>
                {
                    new RegistrySort("NotExistingField", SortDirection.Asc)
                }
            };

            // Act
            var result = await selector.Query(query).ToRegistryPage();

            // Assert
            Assert.Equal(1, result.Items.First().Id);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        public void PageInfoModel_SetPageNumber(int pageNumber)
        {
            // Arrange
            var pageInfo = new RegistryPageInfo();

            // Act
            pageInfo.PageNumber = pageNumber;

            // Assert
            Assert.Equal(pageNumber, pageInfo.PageNumber);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void PageInfoModel_SetPageNumber_Wrong(int pageNumber)
        {
            // Arrange
            var pageInfo = new RegistryPageInfo();

            // Act
            void action() => pageInfo.PageNumber = pageNumber;

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(action);
            Assert.Equal(1, pageInfo.PageNumber);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        public void PageInfoModel_SetRowsCount(int rowsCount)
        {
            // Arrange
            var pageInfo = new RegistryPageInfo();

            // Act
            pageInfo.RowsCount = rowsCount;

            // Assert
            Assert.Equal(rowsCount, pageInfo.RowsCount);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void PageInfoModel_SetRowsCount_Wrong(int rowsCount)
        {
            // Arrange
            var pageInfo = new RegistryPageInfo();

            // Act
            void action() => pageInfo.RowsCount = rowsCount;

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(action);
            Assert.Equal(10, pageInfo.RowsCount);
        }

        #region Test Cases

        public static IEnumerable<object[]> FilterData_CheckCount => new List<object[]>
        {
            new object[] { new RegistryQuery<TestFilterModel> (), 8 },
            new object[] { new RegistryQuery<TestFilterModel> { Filter0 = new TestFilterModel () }, 8 },
            new object[] { new RegistryQuery<TestFilterModel> { Filter0 = new TestFilterModel { IsDeleted = true } }, 2 },
            new object[] { new RegistryQuery<TestFilterModel> { Filter0 = new TestFilterModel { IsDeleted = false } }, 8 },
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

        public static IEnumerable<object[]> SortData_Fields_CheckFirstId => new List<object[]>
        {
            new object[] { "Id", SortDirection.Asc, 1 },
            new object[] { "Id", SortDirection.Desc, 8 },
        };

        public static IEnumerable<object[]> SortData_Paging_CheckFirstId => new List<object[]>
        {
            new object[] { 2, 3, 4 },
        };

        #endregion
    }
}
