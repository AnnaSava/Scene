using AutoMapper;
using Framework.Base.DataService.Contract.Models;
using Framework.Base.DataService.Services;
using Framework.Base.Types.Enums;
using Framework.Tests.Base.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Framework.Tests.Base.Actions
{
    public class GetAllTests : IDisposable
    {
        private TestDbContext _dbContext;
        private IMapper _mapper;

        public GetAllTests()
        {
            _dbContext = GetContext();
            _mapper = new MapperConfiguration(opts => { opts.AddProfile<TestAutoMapperProfile>(); }).CreateMapper();
            FillContextWithTestData(_dbContext, TestData.GetNewEntities());
        }

        public void Dispose()
        {
            _mapper = null;
            _dbContext = null;
        }

        [Theory]
        [MemberData(nameof(FilterData_CheckCount))]
        public async Task GetAll_CheckCount(ListQueryModel<TestFilterModel> query, long expectedCount)
        {
            // Arrange

            // Act
            var result = await _dbContext.GetAll<TestEntity, TestModel, TestFilterModel>(query, TestData.ApplyFilters, _mapper);

            // Assert
            Assert.Equal(expectedCount, result.TotalRows);
        }

        [Theory]
        [MemberData(nameof(FilterData_Ids))]
        [MemberData(nameof(FilterData_Ids_Ext))]
        public async Task GetAll_ById_CheckIds(MatchModeNumeric matchMode, List<long> values, long[] expectedIds)
        {
            // Arrange
            var query = new ListQueryModel<TestFilterModel>
            {
                Filter = new TestFilterModel
                {
                    Ids = new NumericFilterField<long>
                    {
                        MatchMode = matchMode,
                        Value = values
                    }
                }
            };

            // Act
            var result = await _dbContext.GetAll<TestEntity, TestModel, TestFilterModel>(query, TestData.ApplyFilters, _mapper);

            // Assert
            Assert.Equal(expectedIds, result.Items.Select(m => m.Id).ToArray());
        }

        [Theory]
        [MemberData(nameof(FilterData_Ids_NotFound))]
        public async Task GetAll_ById_NotFound(MatchModeNumeric matchMode, List<long> values)
        {
            // Arrange
            var query = new ListQueryModel<TestFilterModel>
            {
                Filter = new TestFilterModel
                {
                    Ids = new NumericFilterField<long>
                    {
                        MatchMode = matchMode,
                        Value = values
                    }
                }
            };

            // Act
            var result = await _dbContext.GetAll<TestEntity, TestModel, TestFilterModel>(query, TestData.ApplyFilters, _mapper);

            // Assert
            Assert.Equal(0, result.TotalRows);
        }

        [Fact]
        public async Task GetAll_ById_MatchModeNotSet()
        {
            // Arrange
            var query = new ListQueryModel<TestFilterModel>
            {
                Filter = new TestFilterModel
                {
                    Ids = new NumericFilterField<long>()
                }
            };
            var expectedIds = new long[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            // Act
            var result = await _dbContext.GetAll<TestEntity, TestModel, TestFilterModel>(query, TestData.ApplyFilters, _mapper);

            // Assert
            Assert.Equal(expectedIds, result.Items.Select(m => m.Id).ToArray());
        }

        [Theory]
        [MemberData(nameof(SortData_Fields_CheckFirstId))]
        public async Task GetAll_Sort_CheckFirstId(string fieldName, SortDirection direction, long expectedFirstId)
        {
            // Arrange
            var query = new ListQueryModel<TestFilterModel>
            {
                PageInfo = new PageInfoModel
                {
                    Sort = new List<ListSortModel>
                        {
                            new ListSortModel { FieldName = fieldName, Direction = direction}
                        }
                }
            };

            // Act
            var result = await _dbContext.GetAll<TestEntity, TestModel, TestFilterModel>(query, TestData.ApplyFilters, _mapper);

            // Assert
            Assert.Equal(expectedFirstId, result.Items.First().Id);
        }

        [Theory]
        [MemberData(nameof(SortData_Paging_CheckFirstId))]
        public async Task GetAll_SortIdAsc_Paging_CheckFirstId(int pageNumber, int rowsCount, long expectedFirstId)
        {
            // Arrange
            var query = new ListQueryModel<TestFilterModel>
            {
                PageInfo = new PageInfoModel
                {
                    PageNumber = pageNumber,
                    RowsCount = rowsCount,
                    Sort = new List<ListSortModel>
                    {
                        new ListSortModel { FieldName = "Id", Direction = SortDirection.Asc }
                    }
                }
            };

            // Act
            var result = await _dbContext.GetAll<TestEntity, TestModel, TestFilterModel>(query, TestData.ApplyFilters, _mapper);

            // Assert
            Assert.Equal(expectedFirstId, result.Items.First().Id);
            Assert.Equal(pageNumber, result.Page);
        }

        [Theory]
        [MemberData(nameof(SortData_Paging_CheckFirstId))]
        public async Task GetAll_SortDefault_Paging_CheckFirstId(int pageNumber, int rowsCount, long expectedFirstId)
        {
            // Arrange
            var query = new ListQueryModel<TestFilterModel>
            {
                PageInfo = new PageInfoModel
                {
                    PageNumber = pageNumber,
                    RowsCount = rowsCount,
                }
            };

            // Act
            var result = await _dbContext.GetAll<TestEntity, TestModel, TestFilterModel>(query, TestData.ApplyFilters, _mapper);

            // Assert
            Assert.Equal(expectedFirstId, result.Items.First().Id);
            Assert.Equal(pageNumber, result.Page);
        }

        // TODO проработать кейс
        //[Fact]
        private async Task GetAll_Sort_WrongField_CheckFirstId()
        {
            // Arrange
            var query = new ListQueryModel<TestFilterModel>
            {
                PageInfo = new PageInfoModel
                {
                    Sort = new List<ListSortModel>
                        {
                            new ListSortModel { FieldName = "NotExistingField", Direction = SortDirection.Asc }
                        }
                }
            };

            // Act
            var result = await _dbContext.GetAll<TestEntity, TestModel, TestFilterModel>(query, TestData.ApplyFilters, _mapper);

            // Assert
            Assert.Equal(1, result.Items.First().Id);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        public void PageInfoModel_SetPageNumber(int pageNumber)
        {
            // Arrange
            var pageInfo = new PageInfoModel();

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
            var pageInfo = new PageInfoModel();

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
            var pageInfo = new PageInfoModel();

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
            var pageInfo = new PageInfoModel();

            // Act
            void action() => pageInfo.RowsCount = rowsCount;

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(action);
            Assert.Equal(10, pageInfo.RowsCount);
        }

        #region Init

        private TestDbContext GetContext()
        {
            var options = GetOptionsAction();

            var optionsBuilder = new DbContextOptionsBuilder<TestDbContext>();
            options.Invoke(optionsBuilder);

            return new TestDbContext(optionsBuilder.Options);
        }

        private Action<DbContextOptionsBuilder> GetOptionsAction() => options => options.UseInMemoryDatabase(Guid.NewGuid().ToString());

        private void FillContextWithTestData(TestDbContext context, IEnumerable<TestEntity> data)
        {
            context.Database.EnsureCreated();
            context.TestEntities.AddRange(data);
            context.SaveChanges();
        }

        #endregion

        #region Test Cases

        public static IEnumerable<object[]> FilterData_CheckCount => new List<object[]>
        {
            new object[] { new ListQueryModel<TestFilterModel> (), 8 },
            new object[] { new ListQueryModel<TestFilterModel> { Filter = new TestFilterModel () }, 8 },
            new object[] { new ListQueryModel<TestFilterModel> { Filter = new TestFilterModel { IsDeleted = true } }, 2 },
            new object[] { new ListQueryModel<TestFilterModel> { Filter = new TestFilterModel { IsDeleted = false } }, 8 },
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
