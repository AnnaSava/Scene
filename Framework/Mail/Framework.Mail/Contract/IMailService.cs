namespace Framework.Mail
{
    public interface IMailService
    {
        Task FormatAndSendEmail(MailDataReceivedModel data);
    }
}
