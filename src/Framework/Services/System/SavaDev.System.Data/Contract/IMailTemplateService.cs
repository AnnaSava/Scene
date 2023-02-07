using SavaDev.Base.Data.Services;
using SavaDev.Base.Data.Services.Interfaces;
using SavaDev.System.Data.Contract.Models;

namespace SavaDev.System.Data.Contract
{
    public interface IMailTemplateService : IEntityRegistryService<MailTemplateModel, MailTemplateFilterModel>
    {
        Task<OperationResult> Create(MailTemplateModel model);

        Task<OperationResult> CreateTranslation(MailTemplateModel model);

        Task<OperationResult> Update(long id, MailTemplateModel model);

        Task<OperationResult> Publish(long id);

        Task<OperationResult> CreateVersion(MailTemplateModel model);

        Task<OperationResult> Delete(long id);

        Task<OperationResult> Restore(long id);

        Task<MailTemplateModel> GetOne<MailTemplateModel>(long id);

        Task<MailTemplateModel> GetActual<MailTemplateModel>(string permName, string culture);

        Task<bool> CheckPermNameExists(string permName);

        Task<bool> CheckTranslationExists(string permName, string culture);

        Task<bool> CheckHasAllTranslations(string permName);

        Task<IEnumerable<string>> GetMissingCultures(string permName);
    }
}
