using SavaDev.Mail.Service.Contract.Models;

namespace SavaDev.Mail.Service.Contract
{
    public interface IMailService
    {
        Task<MailSendResult> FormatAndSendEmail(MailDataModel data);
    }
}
