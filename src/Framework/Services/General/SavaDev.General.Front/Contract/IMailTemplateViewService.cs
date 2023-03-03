using SavaDev.Base.Data.Registry;
using SavaDev.Base.Front.Registry;
using SavaDev.General.Front.Contract.Models;

namespace SavaDev.General.Front.Contract
{
    public interface IMailTemplateViewService
    {
        Task<TResult> GetOne<TResult>(long id);

        Task<TResult> GetActual<TResult>(string permName, string culture);

        Task<MailTemplateViewModel> Create(MailTemplateFormViewModel model);

        Task<MailTemplateViewModel> CreateTranslation(MailTemplateFormViewModel model);

        Task<MailTemplateViewModel> Update(long id, MailTemplateFormViewModel model);

        Task Publish(long id);

        Task<MailTemplateViewModel> CreateVersion(MailTemplateFormViewModel model);

        Task<MailTemplateViewModel> Delete(long id);

        Task<MailTemplateViewModel> Restore(long id);

        Task<bool> CheckPermNameExists(string permName);

        Task<bool> CheckTranslationExists(string permName, string culture);

        Task<IEnumerable<string>> GetMissingCultures(string permName);

        Task<MailTemplateFormattedViewModel> FormatMail(string permName, string culture, IDictionary<string, string> vars);

        Task<RegistryPageViewModel<MailTemplateViewModel>> GetRegistryPage(RegistryQuery query);
    }
}
