using AutoMapper;
using Framework.Base.DataService.Exceptions;
using Framework.Base.Types.Enums;
using Framework.MailTemplate.Data.Contract.Models;
using Framework.MailTemplate.Data.Mapper;
using Framework.MailTemplate.Data.Services;
using Framework.Tests.Base.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Framework.Tests.Mail
{
    public class MailTemplateDbServiceTests : IDisposable
    {
        private IMapper _mapper;
        private MailTemplateTestDbContext _context;
        private MailTemplateDbService _mailTemplateDbService;
        private readonly string[] _cultures = new string[] { "en", "ru" };

        public MailTemplateDbServiceTests()
        {
            _mapper = new MapperConfiguration(opts => { opts.AddProfile<MailTemplateDataMapperProfile>(); }).CreateMapper();
            _context = Infrastructure.GetContext<MailTemplateTestDbContext>(x => new MailTemplateTestDbContext(x));

            // TODO пробросить логгер и культуры
            _mailTemplateDbService = new MailTemplateDbService(_context, _cultures, _mapper, null);
            FillContextWithTestData(_context, TestData.GetMailTemplates());
        }

        public void Dispose()
        {
            _mapper = null;
            _context = null;
            _mailTemplateDbService = null;
        }

        [Fact]
        public async Task Create_Ok()
        {
            // Arrange
            var mailTemplateModel = new MailTemplateModel() { PermName = "doc99", Culture = "en" };
            var testStartDate = DateTime.Now;

            // Act
            var result = await _mailTemplateDbService.Create(mailTemplateModel);
            var model = result.Model;

            // Assert
            Assert.IsType<MailTemplateModel>(model);
            Assert.Equal(mailTemplateModel.PermName, model.PermName);
            Assert.Equal(DocumentStatus.Draft, model.Status);
            Assert.False(model.IsDeleted);
            Assert.Equal(1, _context.MailTemplates.Count(m => m.PermName == model.PermName));
            Assert.True(_context.MailTemplates.First(m => m.Id == model.Id).Created > testStartDate);
            Assert.True(_context.MailTemplates.First(m => m.Id == model.Id).LastUpdated > testStartDate);
        }

        [Theory]
        [MemberData(nameof(Data_PermNameExists))]
        public async Task Create_PermNameExists(string permName, string culture)
        {
            // Arrange
            var mailTemplateModel = new MailTemplateModel() { PermName = permName, Culture = culture };

            // Act
            async Task action() => await _mailTemplateDbService.Create(mailTemplateModel);

            // Assert
            await Assert.ThrowsAsync<InvalidOperationException>(action);
        }

        [Theory]
        [MemberData(nameof(Data_TranslationOk))]
        public async Task CreateTranslation_Ok(string permName, string culture)
        {
            // Arrange
            var mailTemplateModel = new MailTemplateModel() { PermName = permName, Culture = culture };
            var testStartDate = DateTime.Now;

            // Act
            var result = await _mailTemplateDbService.CreateTranslation(mailTemplateModel);
            var model = result.Model;

            // Assert
            Assert.IsType<MailTemplateModel>(model);
            Assert.Equal(mailTemplateModel.PermName, model.PermName);
            Assert.Equal(mailTemplateModel.Culture, model.Culture);
            Assert.Equal(DocumentStatus.Draft, model.Status);
            Assert.False(model.IsDeleted);
            Assert.Equal(1, _context.MailTemplates.Count(m => m.PermName == model.PermName && m.Culture == model.Culture));
            Assert.True(_context.MailTemplates.First(m => m.Id == model.Id).Created > testStartDate);
            Assert.True(_context.MailTemplates.First(m => m.Id == model.Id).LastUpdated > testStartDate);
        }

        [Theory]
        [MemberData(nameof(Data_TranslationExists))]
        public async Task CreateTranslation_Exists(string permName, string culture)
        {
            // Arrange
            var mailTemplateModel = new MailTemplateModel() { PermName = permName, Culture = culture };

            // Act
            async Task action() => await _mailTemplateDbService.CreateTranslation(mailTemplateModel);

            // Assert
            await Assert.ThrowsAsync<InvalidOperationException>(action);
        }

        [Theory]
        [MemberData(nameof(Data_VersionOk))]
        public async Task CreateVersion_Ok(string permName, string culture)
        {
            // Arrange
            var mailTemplateModel = new MailTemplateModel() { PermName = permName, Culture = culture };
            var testStartDate = DateTime.Now;

            // Act
            var result = await _mailTemplateDbService.CreateVersion(mailTemplateModel);
            var model = result.Model;

            // Assert
            Assert.IsType<MailTemplateModel>(model);
            Assert.Equal(mailTemplateModel.PermName, model.PermName);
            Assert.Equal(mailTemplateModel.Culture, model.Culture);
            Assert.Equal(DocumentStatus.Draft, model.Status);
            Assert.False(model.IsDeleted);
            Assert.Equal(1, _context.MailTemplates.Count(m => m.PermName == model.PermName && m.Culture == model.Culture && m.Status == DocumentStatus.Draft && m.IsDeleted == false));
            Assert.True(_context.MailTemplates.First(m => m.Id == model.Id).Created > testStartDate);
            Assert.True(_context.MailTemplates.First(m => m.Id == model.Id).LastUpdated > testStartDate);
        }

        [Theory]
        [MemberData(nameof(Data_Version_DraftExists))]
        public async Task CreateVersion_DraftExists(string permName, string culture)
        {
            // Arrange
            var mailTemplateModel = new MailTemplateModel() { PermName = permName, Culture = culture };
            var testStartDate = DateTime.Now;

            // Act
            async Task action() => await _mailTemplateDbService.CreateVersion(mailTemplateModel);

            // Assert
            await Assert.ThrowsAsync<InvalidOperationException>(action);
        }

        [Theory]
        [MemberData(nameof(Data_UpdateOk))]
        public async Task Update_Ok(MailTemplateModel mailTemplateModel)
        {
            // Arrange
            mailTemplateModel.Title = "Title";
            mailTemplateModel.Text = "Text";
            var testStartDate = DateTime.Now;
            var createdDate = new DateTime(2000, 1, 1, 0, 0, 0);
            var oldModel = _context.MailTemplates.AsNoTracking().First(m => m.Id == mailTemplateModel.Id);

            // Act
            var result = await _mailTemplateDbService.Update(mailTemplateModel.Id, mailTemplateModel);
            var model = result.Model;

            // Assert
            Assert.IsType<MailTemplateModel>(model);
            Assert.Equal(oldModel.PermName, model.PermName);
            Assert.Equal(oldModel.Culture, model.Culture);
            Assert.Equal(DocumentStatus.Draft, model.Status);
            Assert.False(model.IsDeleted);
            Assert.Equal(1, _context.MailTemplates.Count(m => m.PermName == model.PermName && m.Culture == model.Culture && m.Status == DocumentStatus.Draft && m.IsDeleted == false));
            Assert.True(_context.MailTemplates.First(m => m.Id == model.Id).Created == createdDate);
            Assert.True(_context.MailTemplates.First(m => m.Id == model.Id).LastUpdated > testStartDate);

            Assert.Equal(mailTemplateModel.Title, model.Title);
            Assert.Equal(mailTemplateModel.Text, model.Text);
        }

        [Theory]
        [MemberData(nameof(Data_UpdateNotExists))]
        public async Task Update_NotExists(long id)
        {
            // Arrange
            var mailTemplateModel = new MailTemplateModel { Id = id, Title = "Title", Text = "Text"};

            // Act
            async Task action() => await _mailTemplateDbService.Update(mailTemplateModel.Id, mailTemplateModel);

            // Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(action);
        }

        [Theory]
        [MemberData(nameof(Data_UpdateErrorStatus))]
        public async Task Update_ErrorStatus(long id)
        {
            // Arrange
            var mailTemplateModel = new MailTemplateModel { Id = id, Title = "Title", Text = "Text" };

            // Act
            async Task action() => await _mailTemplateDbService.Update(mailTemplateModel.Id, mailTemplateModel);

            // Assert
            await Assert.ThrowsAsync<Exception>(action);
        }

        [Fact]
        public async Task Publish_Ok()
        {
            // Arrange
            var id = 3;

            // Act
            await _mailTemplateDbService.Publish(id);

            // Assert
            var entity = _context.MailTemplates.First(m => m.Id == id);
            Assert.Equal(DocumentStatus.Published, entity.Status);
            Assert.Equal(0, _context
                .MailTemplates
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
            async Task action() => await _mailTemplateDbService.Publish(id);

            // Assert
            await Assert.ThrowsAsync<Exception>(action);
        }

        [Theory]
        [MemberData(nameof(Data_UpdateNotExists))]
        public async Task Publish_ErrorNotExists(long id)
        {
            // Arrange

            // Act
            async Task action() => await _mailTemplateDbService.Publish(id);

            // Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(action);
        }

        [Theory]
        [MemberData(nameof(Data_DeleteOk))]
        public async Task Delete_Ok(long id)
        {
            // Arrange

            // Act
            await _mailTemplateDbService.Delete(id);

            // Assert
            Assert.True(_context.MailTemplates.Any(m => m.Id == id && m.IsDeleted));
        }

        [Theory]
        [MemberData(nameof(Data_UpdateNotExists))]
        public async Task Delete_NotExists(long id)
        {
            // Arrange

            // Act
            async Task action() => await _mailTemplateDbService.Delete(id);

            // Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(action);
        }

        [Theory]
        [MemberData(nameof(Data_RestoreOk))]
        public async Task Restore_Ok(long id)
        {
            // Arrange

            // Act
            await _mailTemplateDbService.Restore(id);

            // Assert
            Assert.True(_context.MailTemplates.Any(m => m.Id == id && m.IsDeleted == false));
        }

        [Fact]
        public async Task Restore_NotExists()
        {
            // Arrange
            var id = 99;

            // Act
            async Task action() => await _mailTemplateDbService.Restore(id);

            // Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(action);
        }

        [Fact]
        public async Task Restore_NotDeleted()
        {
            // Arrange
            var id = 17;

            // Act
            async Task action() => await _mailTemplateDbService.Restore(id);

            // Assert
            await Assert.ThrowsAsync<InvalidOperationException>(action);
        }

        [Fact]
        public async Task Restore_NotSingleDraft()
        {
            // Arrange
            var id = 14;

            // Act
            async Task action() => await _mailTemplateDbService.Restore(id);

            // Assert
            await Assert.ThrowsAsync<InvalidOperationException>(action);
        }

        [Fact]
        public async Task Restore_NotSinglePublished()
        {
            // Arrange
            var id = 18;

            // Act
            async Task action() => await _mailTemplateDbService.Restore(id);

            // Assert
            await Assert.ThrowsAsync<InvalidOperationException>(action);
        }

        [Theory]
        [MemberData(nameof(Data_DeleteOk))]
        public async Task GetOne_Ok(long id)
        {
            // Arrange

            // Act
            var model = await _mailTemplateDbService.GetOne(id);

            // Assert
            Assert.NotNull(model);
        }

        [Fact]
        public async Task GetOne_DeletedOk()
        {
            // Arrange
            var id = 9;

            // Act
            var model = await _mailTemplateDbService.GetOne(id);

            // Assert
            Assert.NotNull(model);
        }

        [Fact]
        public async Task GetOne_NotExists()
        {
            // Arrange
            var id = 99;

            // Act
            var model = await _mailTemplateDbService.GetOne(id);

            // Assert
            Assert.Null(model);
        }

        [Theory]
        [MemberData(nameof(Data_GetActualOk))]
        public async Task GetActual_Ok(string permName)
        {
            // Arrange
            var culture = "en";

            // Act
            var model = await _mailTemplateDbService.GetActual(permName, culture);

            // Assert
            Assert.NotNull(model);
            Assert.Equal(DocumentStatus.Published, model.Status);
            Assert.False(model.IsDeleted);
        }

        [Theory]
        [MemberData(nameof(Data_GetActual_None))]
        public async Task GetActual_None(string permName)
        {
            // Arrange
            var culture = "en";

            // Act
            var model = await _mailTemplateDbService.GetActual(permName, culture);

            // Assert
            Assert.Null(model);
        }

        [Theory]
        [MemberData(nameof(Data_CheckDocumentExisis_True))]
        public async Task CheckDocumentExists_True(string permName)
        {
            // Arrange

            // Act
            var result = await _mailTemplateDbService.CheckPermNameExists(permName);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task CheckDocumentExists_False()
        {
            // Arrange
            string permName = "doc999";

            // Act
            var result = await _mailTemplateDbService.CheckPermNameExists(permName);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task CheckTraslationExists_True()
        {
            // Arrange
            string permName = "doc1";
            var culture = "en";

            // Act
            var result = await _mailTemplateDbService.CheckTranslationExists(permName, culture);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task CheckTraslationExists_False()
        {
            // Arrange
            string permName = "doc2";
            var culture = "ru";

            // Act
            var result = await _mailTemplateDbService.CheckTranslationExists(permName, culture);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task CheckHasAllTraslations_True()
        {
            // Arrange
            string permName = "doc1";

            // Act
            var result = await _mailTemplateDbService.CheckHasAllTranslations(permName);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task CheckHasAllTraslations_False()
        {
            // Arrange
            string permName = "doc2";

            // Act
            var result = await _mailTemplateDbService.CheckHasAllTranslations(permName);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task GetMissingCultures_Ok()
        {
            // Arrange
            string permName = "doc2";

            // Act
            var result = await _mailTemplateDbService.GetMissingCultures(permName);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(new string[] { "ru" }, result);
        }

        [Fact]
        public async Task GetMissingCultures_NotExists()
        {
            // Arrange
            string permName = "doc999";

            // Act
            var result = await _mailTemplateDbService.GetMissingCultures(permName);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(new string[] { "en", "ru" }, result);
        }

        [Fact]
        public async Task GetMissingCultures_Full()
        {
            // Arrange
            string permName = "doc1";

            // Act
            var result = await _mailTemplateDbService.GetMissingCultures(permName);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        private void FillContextWithTestData(MailTemplateTestDbContext context, IEnumerable<MailTemplate.Data.Entities.MailTemplate> data)
        {
            context.Database.EnsureCreated();
            context.MailTemplates.AddRange(data);
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
            new object[] { new MailTemplateModel { Id = 3 } },
            new object[] { new MailTemplateModel { Id = 3, Culture = "ru", PermName = "doc99", Status = DocumentStatus.Published } },
            new object[] { new MailTemplateModel { Id = 3, Culture = "ru", Created = new DateTime(2001,1,1), LastUpdated = new DateTime(2001,1,1) } },
            new object[] { new MailTemplateModel { Id = 3, Culture = "ru", IsDeleted = true } },
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

        public static IEnumerable<object[]> Data_DeleteOk => new List<object[]>
        {
            new object[] { 3 }, // Draft
            new object[] { 2 }, // Published
            new object[] { 1 }, // Outdated
        };

        public static IEnumerable<object[]> Data_RestoreOk => new List<object[]>
        {
            new object[] { 9 }, // Single Draft
            new object[] { 15 }, // Outdated
            new object[] { 16 }, // Single Published
        };

        public static IEnumerable<object[]> Data_GetActualOk => new List<object[]>
        {
            new object[] { "doc8" },
        };

        public static IEnumerable<object[]> Data_GetActual_None => new List<object[]>
        {
            new object[] { "doc3" },
            new object[] { "doc7" },
        };

        public static IEnumerable<object[]> Data_CheckDocumentExisis_True => new List<object[]>
        {
            new object[] { "doc1" },
            new object[] { "doc2" },
            new object[] { "doc3" },
        };
    }
}
