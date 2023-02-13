using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SavaDev.Base.Data.Enums;
using SavaDev.Base.Data.Exceptions;
using SavaDev.Base.Data.Services;
using SavaDev.Libs.UnitTestingHelpers;
using SavaDev.Services.Tests.System.Data;
using SavaDev.System.Data;
using SavaDev.System.Data.Contract.Models;
using SavaDev.System.Data.Services;

namespace SavaDev.Services.Tests.System.DataTests
{
    public class LegalDocumentServiceTests : IDisposable
    {
        private IMapper _mapper;
        private ILogger<LegalDocumentService> _logger;
        private SystemContext _context;
        private LegalDocumentService _legalDocumentService;
        private readonly string[] _cultures = new string[] { "en", "ru" };

        public LegalDocumentServiceTests()
        {
            _mapper = Dependencies.GetDataMapper();
            _logger = TestsInfrastructure.GetLogger<LegalDocumentService>();
            _context = TestsInfrastructure.GetContext<SystemContext>(x => new SystemContext(x));
            DataInit.FillContextWithEntities(_context);
            _legalDocumentService = new LegalDocumentService(_context, _cultures, _mapper, _logger);
        }

        public void Dispose()
        {
            _mapper = null;
            _context = null;
            _legalDocumentService = null;
            _logger = null;
        }

        #region Create 

        [Theory]
        [MemberData(nameof(DataInit.GetLegalDocumentsInput), nameof(Create_Ok), MemberType = typeof(DataInit))]
        public async Task Create_Ok(LegalDocumentModel legalDocumentModel)
        {
            // Arrange
            var testStartDate = DateTime.UtcNow;

            // Act
            var result = await _legalDocumentService.Create(legalDocumentModel);            

            // Assert
            Assert.IsType<LegalDocumentModel>(result.ProcessedObject);
            var model = result.ProcessedObject as LegalDocumentModel;
            Assert.Equal(legalDocumentModel.PermName, model.PermName);
            Assert.Equal(DocumentStatus.Draft, model.Status);
            Assert.False(model.IsDeleted);
            Assert.Equal(1, _context.LegalDocuments.Count(m => m.PermName == model.PermName));
            Assert.True(_context.LegalDocuments.First(m => m.Id == model.Id).Created > testStartDate);
            Assert.True(_context.LegalDocuments.First(m => m.Id == model.Id).LastUpdated > testStartDate);
        }

        [Theory]
        [MemberData(nameof(DataInit.GetLegalDocumentsInput), nameof(Create_PermNameExists), MemberType = typeof(DataInit))]
        public async Task Create_PermNameExists(LegalDocumentModel legalDocumentModel)
        {
            // Arrange

            // Act
            async Task action() => await _legalDocumentService.Create(legalDocumentModel);

            // Assert
            await Assert.ThrowsAsync<InvalidOperationException>(action);
        }

        #endregion

        #region CreateTranslation 

        [Theory]
        [MemberData(nameof(DataInit.GetLegalDocumentsInput), nameof(CreateTranslation_Ok), MemberType = typeof(DataInit))]
        public async Task CreateTranslation_Ok(LegalDocumentModel legalDocumentModel)
        {
            // Arrange
            var testStartDate = DateTime.UtcNow;

            // Act
            var result = await _legalDocumentService.CreateTranslation(legalDocumentModel);            

            // Assert
            Assert.IsType<LegalDocumentModel>(result.ProcessedObject);
            var model = result.ProcessedObject as LegalDocumentModel;
            Assert.Equal(legalDocumentModel.PermName, model.PermName);
            Assert.Equal(legalDocumentModel.Culture, model.Culture);
            Assert.Equal(DocumentStatus.Draft, model.Status);
            Assert.False(model.IsDeleted);
            Assert.Equal(1, _context.LegalDocuments.Count(m => m.PermName == model.PermName && m.Culture == model.Culture));
            Assert.True(_context.LegalDocuments.First(m => m.Id == model.Id).Created > testStartDate);
            Assert.True(_context.LegalDocuments.First(m => m.Id == model.Id).LastUpdated > testStartDate);
        }

        [Theory]
        [MemberData(nameof(DataInit.GetLegalDocumentsInput), nameof(CreateTranslation_Exists), MemberType = typeof(DataInit))]
        public async Task CreateTranslation_Exists(LegalDocumentModel legalDocumentModel)
        {
            // Arrange

            // Act
            async Task action() => await _legalDocumentService.CreateTranslation(legalDocumentModel);

            // Assert
            await Assert.ThrowsAsync<InvalidOperationException>(action);
        }

        #endregion

        #region CreateVersion 

        [Theory]
        [MemberData(nameof(DataInit.GetLegalDocumentsInput), nameof(CreateVersion_Ok), MemberType = typeof(DataInit))]
        public async Task CreateVersion_Ok(LegalDocumentModel legalDocumentModel)
        {
            // Arrange
            var testStartDate = DateTime.UtcNow;

            // Act
            var result = await _legalDocumentService.CreateVersion(legalDocumentModel);
            
            // Assert
            Assert.IsType<LegalDocumentModel>(result.ProcessedObject);
            var model = result.ProcessedObject as LegalDocumentModel;
            Assert.Equal(legalDocumentModel.PermName, model.PermName);
            Assert.Equal(legalDocumentModel.Culture, model.Culture);
            Assert.Equal(DocumentStatus.Draft, model.Status);
            Assert.False(model.IsDeleted);
            Assert.Equal(1, _context.LegalDocuments.Count(m => m.PermName == model.PermName && m.Culture == model.Culture && m.Status == DocumentStatus.Draft && m.IsDeleted == false));
            Assert.True(_context.LegalDocuments.First(m => m.Id == model.Id).Created > testStartDate);
            Assert.True(_context.LegalDocuments.First(m => m.Id == model.Id).LastUpdated > testStartDate);
        }

        [Theory]
        [MemberData(nameof(DataInit.GetLegalDocumentsInput), nameof(CreateVersion_DraftExists), MemberType = typeof(DataInit))]
        public async Task CreateVersion_DraftExists(LegalDocumentModel legalDocumentModel)
        {
            // Arrange
            var testStartDate = DateTime.UtcNow;

            // Act
            async Task action() => await _legalDocumentService.CreateVersion(legalDocumentModel);

            // Assert
            await Assert.ThrowsAsync<InvalidOperationException>(action);
        }

        #endregion

        #region Update 

        [Theory]
        [MemberData(nameof(DataInit.GetLegalDocumentsInput), nameof(Update_Ok), MemberType = typeof(DataInit))]
        public async Task Update_Ok(LegalDocumentModel legalDocumentModel)
        {
            // Arrange
            var testStartDate = DateTime.UtcNow;
            var oldModel = _context.LegalDocuments.AsNoTracking().First(m => m.Id == legalDocumentModel.Id);

            // Act
            var result = await _legalDocumentService.Update(legalDocumentModel.Id, legalDocumentModel);
            
            // Assert
            Assert.IsType<LegalDocumentModel>(result.ProcessedObject);
            var model = result.ProcessedObject as LegalDocumentModel;
            Assert.Equal(oldModel.PermName, model.PermName);
            Assert.Equal(oldModel.Culture, model.Culture);
            Assert.Equal(DocumentStatus.Draft, model.Status);
            Assert.False(model.IsDeleted);
            Assert.Equal(1, _context.LegalDocuments.Count(m => m.PermName == model.PermName && m.Culture == model.Culture && m.Status == DocumentStatus.Draft && m.IsDeleted == false));
            Assert.True(oldModel.Created == model.Created);
            Assert.True(_context.LegalDocuments.First(m => m.Id == model.Id).LastUpdated > testStartDate);

            Assert.Equal(legalDocumentModel.Title, model.Title);
            Assert.Equal(legalDocumentModel.Text, model.Text);
            Assert.Equal(legalDocumentModel.Comment, model.Comment);
            Assert.Equal(legalDocumentModel.Info, model.Info);
        }

        [Theory]
        [MemberData(nameof(DataInit.GetLegalDocumentIdsInput), nameof(Update_NotExists), MemberType = typeof(DataInit))]
        public async Task Update_NotExists(long id)
        {
            // Arrange
            var legalDocumentModel = new LegalDocumentModel { Id = id, Title = "Title", Text = "Text", Comment = "Comment", Info = "Info" };

            // Act
            async Task action() => await _legalDocumentService.Update(legalDocumentModel.Id, legalDocumentModel);

            // Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(action);
        }

        [Theory]
        [MemberData(nameof(DataInit.GetLegalDocumentIdsInput), nameof(Update_ErrorStatus), MemberType = typeof(DataInit))]
        public async Task Update_ErrorStatus(long id)
        {
            // Arrange
            var legalDocumentModel = new LegalDocumentModel { Id = id, Title = "Title", Text = "Text", Comment = "Comment", Info = "Info" };

            // Act
            async Task action() => await _legalDocumentService.Update(legalDocumentModel.Id, legalDocumentModel);

            // Assert
            await Assert.ThrowsAsync<Exception>(action);
        }

        #endregion

        #region Publish 

        [Theory]
        [MemberData(nameof(DataInit.GetLegalDocumentIdsInput), nameof(Publish_Ok), MemberType = typeof(DataInit))]
        public async Task Publish_Ok(long id)
        {
            // Arrange

            // Act
            await _legalDocumentService.Publish(id);

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
        [MemberData(nameof(DataInit.GetLegalDocumentIdsInput), nameof(Publish_ErrorStatus), MemberType = typeof(DataInit))]
        public async Task Publish_ErrorStatus(long id)
        {
            // Arrange

            // Act
            async Task action() => await _legalDocumentService.Publish(id);

            // Assert
            await Assert.ThrowsAsync<Exception>(action);
        }

        [Theory]
        [MemberData(nameof(DataInit.GetLegalDocumentIdsInput), nameof(Publish_ErrorNotExists), MemberType = typeof(DataInit))]
        public async Task Publish_ErrorNotExists(long id)
        {
            // Arrange

            // Act
            async Task action() => await _legalDocumentService.Publish(id);

            // Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(action);
        }

        #endregion

        #region Delete 

        [Theory]
        [MemberData(nameof(DataInit.GetLegalDocumentIdsInput), nameof(Delete_Ok), MemberType = typeof(DataInit))]
        public async Task Delete_Ok(long id)
        {
            // Arrange

            // Act
            await _legalDocumentService.Delete(id);

            // Assert
            Assert.True(_context.LegalDocuments.Any(m => m.Id == id && m.IsDeleted));
        }

        [Theory]
        [MemberData(nameof(DataInit.GetLegalDocumentIdsInput), nameof(Delete_NotExists), MemberType = typeof(DataInit))]
        public async Task Delete_NotExists(long id)
        {
            // Arrange

            // Act
            async Task action() => await _legalDocumentService.Delete(id);

            // Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(action);
        }

        #endregion

        #region Restore 

        [Theory]
        [MemberData(nameof(DataInit.GetLegalDocumentIdsInput), nameof(Restore_Ok), MemberType = typeof(DataInit))]
        public async Task Restore_Ok(long id)
        {
            // Arrange

            // Act
            await _legalDocumentService.Restore(id);

            // Assert
            Assert.True(_context.LegalDocuments.Any(m => m.Id == id && m.IsDeleted == false));
        }

        [Theory]
        [MemberData(nameof(DataInit.GetLegalDocumentIdsInput), nameof(Restore_NotExists), MemberType = typeof(DataInit))]
        public async Task Restore_NotExists(long id)
        {
            // Arrange

            // Act
            async Task action() => await _legalDocumentService.Restore(id);

            // Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(action);
        }

        [Theory]
        [MemberData(nameof(DataInit.GetLegalDocumentIdsInput), nameof(Restore_NotDeleted), MemberType = typeof(DataInit))]
        public async Task Restore_NotDeleted(long id)
        {
            // Arrange

            // Act
            async Task action() => await _legalDocumentService.Restore(id);

            // Assert
            await Assert.ThrowsAsync<InvalidOperationException>(action);
        }

        [Theory]
        [MemberData(nameof(DataInit.GetLegalDocumentIdsInput), nameof(Restore_NotSingleDraft), MemberType = typeof(DataInit))]
        public async Task Restore_NotSingleDraft(long id)
        {
            // Arrange

            // Act
            async Task action() => await _legalDocumentService.Restore(id);

            // Assert
            await Assert.ThrowsAsync<InvalidOperationException>(action);
        }

        [Theory]
        [MemberData(nameof(DataInit.GetLegalDocumentIdsInput), nameof(Restore_NotSinglePublished), MemberType = typeof(DataInit))]
        public async Task Restore_NotSinglePublished(long id)
        {
            // Arrange

            // Act
            async Task action() => await _legalDocumentService.Restore(id);

            // Assert
            await Assert.ThrowsAsync<InvalidOperationException>(action);
        }

        #endregion

        #region GetOne

        [Theory]
        [MemberData(nameof(DataInit.GetLegalDocumentIdsInput), nameof(GetOne_Ok), MemberType = typeof(DataInit))]
        public async Task GetOne_Ok(long id)
        {
            // Arrange

            // Act
            var model = await _legalDocumentService.GetOne<LegalDocumentModel>(id);

            // Assert
            Assert.NotNull(model);
        }

        [Theory]
        [MemberData(nameof(DataInit.GetLegalDocumentIdsInput), nameof(GetOne_NotExists), MemberType = typeof(DataInit))]
        public async Task GetOne_NotExists(long id)
        {
            // Arrange

            // Act
            var model = await _legalDocumentService.GetOne<LegalDocumentModel>(id);

            // Assert
            Assert.Null(model);
        }

        #endregion

        #region GetActual

        [Theory]
        [MemberData(nameof(DataInit.GetLegalDocumentPermNamesInput), nameof(GetActual_Ok), MemberType = typeof(DataInit))]
        public async Task GetActual_Ok(string permName)
        {
            // Arrange
            var culture = "en";

            // Act
            var model = await _legalDocumentService.GetActual<LegalDocumentModel>(permName, culture);

            // Assert
            Assert.NotNull(model);
            Assert.Equal(DocumentStatus.Published, model.Status);
            Assert.False(model.IsDeleted);
        }

        [Theory]
        [MemberData(nameof(DataInit.GetLegalDocumentPermNamesInput), nameof(GetActual_None), MemberType = typeof(DataInit))]
        public async Task GetActual_None(string permName)
        {
            // Arrange
            var culture = "en";

            // Act
            var model = await _legalDocumentService.GetActual<LegalDocumentModel>(permName, culture);

            // Assert
            Assert.Null(model);
        }

        #endregion

        #region CheckDocumentExists

        [Theory]
        [MemberData(nameof(DataInit.GetLegalDocumentPermNamesInput), nameof(CheckDocumentExists_True), MemberType = typeof(DataInit))]
        public async Task CheckDocumentExists_True(string permName)
        {
            // Arrange

            // Act
            var result = await _legalDocumentService.CheckPermNameExists(permName);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [MemberData(nameof(DataInit.GetLegalDocumentPermNamesInput), nameof(CheckDocumentExists_False), MemberType = typeof(DataInit))]
        public async Task CheckDocumentExists_False(string permName)
        {
            // Arrange

            // Act
            var result = await _legalDocumentService.CheckPermNameExists(permName);

            // Assert
            Assert.False(result);
        }

        #endregion

        #region CheckTraslationExists

        [Theory]
        [MemberData(nameof(DataInit.GetLegalDocumentPermNamesInput), nameof(CheckTraslationExists_True), MemberType = typeof(DataInit))]
        public async Task CheckTraslationExists_True(string permName)
        {
            // Arrange
            var culture = "en";

            // Act
            var result = await _legalDocumentService.CheckTranslationExists(permName, culture);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [MemberData(nameof(DataInit.GetLegalDocumentPermNamesInput), nameof(CheckTraslationExists_False), MemberType = typeof(DataInit))]
        public async Task CheckTraslationExists_False(string permName)
        {
            // Arrange
            var culture = "ru";

            // Act
            var result = await _legalDocumentService.CheckTranslationExists(permName, culture);

            // Assert
            Assert.False(result);
        }

        #endregion

        #region CheckHasAllTranslations

        [Theory]
        [MemberData(nameof(DataInit.GetLegalDocumentPermNamesInput), nameof(CheckHasAllTraslations_True), MemberType = typeof(DataInit))]
        public async Task CheckHasAllTraslations_True(string permName)
        {
            // Arrange

            // Act
            var result = await _legalDocumentService.CheckHasAllTranslations(permName);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [MemberData(nameof(DataInit.GetLegalDocumentPermNamesInput), nameof(CheckHasAllTraslations_False), MemberType = typeof(DataInit))]
        public async Task CheckHasAllTraslations_False(string permName)
        {
            // Arrange

            // Act
            var result = await _legalDocumentService.CheckHasAllTranslations(permName);

            // Assert
            Assert.False(result);
        }

        #endregion

        #region GetMissingCultures

        [Theory]
        [MemberData(nameof(DataInit.GetLegalDocumentPermNamesInput), nameof(GetMissingCultures_Ok), MemberType = typeof(DataInit))]
        public async Task GetMissingCultures_Ok(string permName)
        {
            // Arrange

            // Act
            var result = await _legalDocumentService.GetMissingCultures(permName);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(new string[] { "ru" }, result);
        }

        [Theory]
        [MemberData(nameof(DataInit.GetLegalDocumentPermNamesInput), nameof(GetMissingCultures_NotExists), MemberType = typeof(DataInit))]
        public async Task GetMissingCultures_NotExists(string permName)
        {
            // Arrange

            // Act
            var result = await _legalDocumentService.GetMissingCultures(permName);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(new string[] { "en", "ru" }, result);
        }

        [Theory]
        [MemberData(nameof(DataInit.GetLegalDocumentPermNamesInput), nameof(GetMissingCultures_Full), MemberType = typeof(DataInit))]
        public async Task GetMissingCultures_Full(string permName)
        {
            // Arrange

            // Act
            var result = await _legalDocumentService.GetMissingCultures(permName);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        #endregion
    }
}
