using AutoMapper;
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
    public class LegalDocumentDbServiceTests : IDisposable
    {
        private IMapper _mapper;
        private LegalDocumentTestDbContext _context;
        private LegalDocumentDbService _legalDocumentDbService;

        public LegalDocumentDbServiceTests()
        {
            _mapper = new MapperConfiguration(opts => { opts.AddProfile<CommonDataMapperProfile>(); }).CreateMapper();
            _context = Infrastructure.GetContext<LegalDocumentTestDbContext>(x => new LegalDocumentTestDbContext(x));
            _legalDocumentDbService = new LegalDocumentDbService(_context, _mapper);
            FillContextWithTestData(_context, TestData.GetLegalDocuments());
        }

        public void Dispose()
        {
            _mapper = null;
            _context = null;
            _legalDocumentDbService = null;
        }

        [Fact]
        public async Task Create_Ok()
        {
            // Arrange
            var legalDocumentModel = new LegalDocumentModel() { PermName = "doc99", Culture = "en" };
            var testStartDate = DateTime.Now;

            // Act
            var model = await _legalDocumentDbService.Create(legalDocumentModel);

            // Assert
            Assert.IsType<LegalDocumentModel>(model);
            Assert.Equal(legalDocumentModel.PermName, model.PermName);
            Assert.Equal(DocumentStatus.Draft, model.Status);
            Assert.False(model.IsDeleted);
            Assert.Equal(1, _context.LegalDocuments.Count(m => m.PermName == model.PermName));
            Assert.True(_context.LegalDocuments.First(m => m.PermName == model.PermName).Created > testStartDate);
            Assert.True(_context.LegalDocuments.First(m => m.PermName == model.PermName).LastUpdated > testStartDate);
        }

        [Theory]
        [MemberData(nameof(Data_PermNameExists))]
        public async Task Create_PermNameExists(string permName, string culture)
        {
            // Arrange
            var legalDocumentModel = new LegalDocumentModel() { PermName = permName, Culture = culture };

            // Act
            async Task action() => await _legalDocumentDbService.Create(legalDocumentModel);

            // Assert
            await Assert.ThrowsAsync<InvalidOperationException>(action);
        }

        [Theory]
        [MemberData(nameof(Data_TranslationOk))]
        public async Task CreateTranslation_Ok(string permName, string culture)
        {
            // Arrange
            var legalDocumentModel = new LegalDocumentModel() { PermName = permName, Culture = culture };
            var testStartDate = DateTime.Now;

            // Act
            var model = await _legalDocumentDbService.CreateTranslation(legalDocumentModel);

            // Assert
            Assert.IsType<LegalDocumentModel>(model);
            Assert.Equal(legalDocumentModel.PermName, model.PermName);
            Assert.Equal(legalDocumentModel.Culture, model.Culture);
            Assert.Equal(DocumentStatus.Draft, model.Status);
            Assert.False(model.IsDeleted);
            Assert.Equal(1, _context.LegalDocuments.Count(m => m.PermName == model.PermName && m.Culture == model.Culture));
            Assert.True(_context.LegalDocuments.First(m => m.PermName == model.PermName && m.Culture == model.Culture).Created > testStartDate);
            Assert.True(_context.LegalDocuments.First(m => m.PermName == model.PermName && m.Culture == model.Culture).LastUpdated > testStartDate);
        }

        [Theory]
        [MemberData(nameof(Data_TranslationExists))]
        public async Task CreateTranslation_Exists(string permName, string culture)
        {
            // Arrange
            var legalDocumentModel = new LegalDocumentModel() { PermName = permName, Culture = culture };

            // Act
            async Task action() => await _legalDocumentDbService.CreateTranslation(legalDocumentModel);

            // Assert
            await Assert.ThrowsAsync<InvalidOperationException>(action);
        }

        private void FillContextWithTestData(LegalDocumentTestDbContext context, IEnumerable<LegalDocument> data)
        {
            context.Database.EnsureCreated();
            context.LegalDocuments.AddRange(data);
            context.SaveChanges();
        }

        public static IEnumerable<object[]> Data_PermNameExists => new List<object[]>
        {
            new object[] { "doc1", "en" },
            new object[] { "doc2", "ru" },
            new object[] { "doc3", "en" },
            new object[] { "doc3", "ru" },
        };

        public static IEnumerable<object[]> Data_TranslationOk => new List<object[]>
        {
            new object[] { "doc2", "ru" },
            new object[] { "doc3", "ru" },
        };

        public static IEnumerable<object[]> Data_TranslationExists => new List<object[]>
        {
            new object[] { "doc1", "en" },
            new object[] { "doc3", "en" },
        };
    }
}
