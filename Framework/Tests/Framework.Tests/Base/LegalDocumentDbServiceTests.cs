using AutoMapper;
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
            Assert.True(_context.LegalDocuments.First(m => m.Id == model.Id).Created > testStartDate);
            Assert.True(_context.LegalDocuments.First(m => m.Id == model.Id).LastUpdated > testStartDate);
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
            Assert.True(_context.LegalDocuments.First(m => m.Id == model.Id).Created > testStartDate);
            Assert.True(_context.LegalDocuments.First(m => m.Id == model.Id).LastUpdated > testStartDate);
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

        [Theory]
        [MemberData(nameof(Data_VersionOk))]
        public async Task CreateVersion_Ok(string permName, string culture)
        {
            // Arrange
            var legalDocumentModel = new LegalDocumentModel() { PermName = permName, Culture = culture };
            var testStartDate = DateTime.Now;

            // Act
            var model = await _legalDocumentDbService.CreateVersion(legalDocumentModel);

            // Assert
            Assert.IsType<LegalDocumentModel>(model);
            Assert.Equal(legalDocumentModel.PermName, model.PermName);
            Assert.Equal(legalDocumentModel.Culture, model.Culture);
            Assert.Equal(DocumentStatus.Draft, model.Status);
            Assert.False(model.IsDeleted);
            Assert.Equal(1, _context.LegalDocuments.Count(m => m.PermName == model.PermName && m.Culture == model.Culture && m.Status == DocumentStatus.Draft && m.IsDeleted == false));
            Assert.True(_context.LegalDocuments.First(m => m.Id == model.Id).Created > testStartDate);
            Assert.True(_context.LegalDocuments.First(m => m.Id == model.Id).LastUpdated > testStartDate);
        }

        [Theory]
        [MemberData(nameof(Data_Version_DraftExists))]
        public async Task CreateVersion_DraftExists(string permName, string culture)
        {
            // Arrange
            var legalDocumentModel = new LegalDocumentModel() { PermName = permName, Culture = culture };
            var testStartDate = DateTime.Now;

            // Act
            async Task action() => await _legalDocumentDbService.CreateVersion(legalDocumentModel);

            // Assert
            await Assert.ThrowsAsync<InvalidOperationException>(action);
        }

        [Theory]
        [MemberData(nameof(Data_UpdateOk))]
        public async Task Update_Ok(LegalDocumentModel legalDocumentModel)
        {
            // Arrange
            legalDocumentModel.Comment = "Comment";
            legalDocumentModel.Info = "Info";
            legalDocumentModel.Title = "Title";
            legalDocumentModel.Text = "Text";
            var testStartDate = DateTime.Now;
            var createdDate = new DateTime(2000, 1, 1, 0, 0, 0);
            var oldModel = _context.LegalDocuments.AsNoTracking().First(m => m.Id == legalDocumentModel.Id);

            // Act
            var model = await _legalDocumentDbService.Update(legalDocumentModel);

            // Assert
            Assert.IsType<LegalDocumentModel>(model);
            Assert.Equal(oldModel.PermName, model.PermName);
            Assert.Equal(oldModel.Culture, model.Culture);
            Assert.Equal(DocumentStatus.Draft, model.Status);
            Assert.False(model.IsDeleted);
            Assert.Equal(1, _context.LegalDocuments.Count(m => m.PermName == model.PermName && m.Culture == model.Culture && m.Status == DocumentStatus.Draft && m.IsDeleted == false));
            Assert.True(_context.LegalDocuments.First(m => m.Id == model.Id).Created == createdDate);
            Assert.True(_context.LegalDocuments.First(m => m.Id == model.Id).LastUpdated > testStartDate);

            Assert.Equal(legalDocumentModel.Title, model.Title);
            Assert.Equal(legalDocumentModel.Text, model.Text);
            Assert.Equal(legalDocumentModel.Comment, model.Comment);
            Assert.Equal(legalDocumentModel.Info, model.Info);
        }

        [Theory]
        [MemberData(nameof(Data_UpdateNotExists))]
        public async Task Update_NotExists(long id)
        {
            // Arrange
            var legalDocumentModel = new LegalDocumentModel { Id = id, Title = "Title", Text = "Text", Comment = "Comment", Info = "Info" };

            // Act
            async Task action() => await _legalDocumentDbService.Update(legalDocumentModel);

            // Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(action);
        }

        [Theory]
        [MemberData(nameof(Data_UpdateErrorStatus))]
        public async Task Update_ErrorStatus(long id)
        {
            // Arrange
            var legalDocumentModel = new LegalDocumentModel { Id = id, Title = "Title", Text = "Text", Comment = "Comment", Info = "Info" };

            // Act
            async Task action() => await _legalDocumentDbService.Update(legalDocumentModel);

            // Assert
            await Assert.ThrowsAsync<Exception>(action);
        }

        [Fact]
        public async Task Publish_Ok()
        {
            // Arrange
            var id = 3;

            // Act
            await _legalDocumentDbService.Publish(id);

            // Assert
            var entity = _context.LegalDocuments.First(m => m.Id == id);
            Assert.Equal(DocumentStatus.Published, entity.Status);
            Assert.Equal(0, _context
                .LegalDocuments
                .Count(m => m.PermName == entity.PermName 
                    && m.Culture == entity.Culture 
                    && m.Status == DocumentStatus.Published 
                    && m.IsDeleted == false 
                    && m.Id != id));
        }

        [Theory]
        [MemberData(nameof(Data_PublishErrorStatus))]
        public async Task Publish_ErrorStatus(long id)
        {
            // Arrange

            // Act
            async Task action() => await _legalDocumentDbService.Publish(id);

            // Assert
            await Assert.ThrowsAsync<Exception>(action);
        }

        [Theory]
        [MemberData(nameof(Data_UpdateNotExists))]
        public async Task Publish_ErrorNotExists(long id)
        {
            // Arrange

            // Act
            async Task action() => await _legalDocumentDbService.Publish(id);

            // Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(action);
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

        public static IEnumerable<object[]> Data_VersionOk => new List<object[]>
        {
            new object[] { "doc3", "en" },
            new object[] { "doc4", "en" },
            new object[] { "doc5", "en" },
        };

        public static IEnumerable<object[]> Data_Version_DraftExists => new List<object[]>
        {
            new object[] { "doc1", "en" },
        };

        public static IEnumerable<object[]> Data_UpdateOk => new List<object[]>
        {
            new object[] { new LegalDocumentModel { Id = 3 } },
            new object[] { new LegalDocumentModel { Id = 3, Culture = "ru", PermName = "doc99", Status = DocumentStatus.Published } },
            new object[] { new LegalDocumentModel { Id = 3, Culture = "ru", Created = new DateTime(2001,1,1), LastUpdated = new DateTime(2001,1,1) } },
            new object[] { new LegalDocumentModel { Id = 3, Culture = "ru", IsDeleted = true } },
        };

        public static IEnumerable<object[]> Data_UpdateNotExists => new List<object[]>
        {
            new object[] { 99 },
            new object[] { 9 }
        };

        public static IEnumerable<object[]> Data_UpdateErrorStatus => new List<object[]>
        {
            new object[] { 11 },
            new object[] { 12 },
            new object[] { 13 }
        };

        public static IEnumerable<object[]> Data_PublishErrorStatus => new List<object[]>
        {
            new object[] { 11 },
            new object[] { 12 },
            new object[] { 13 }
        };
    }
}
