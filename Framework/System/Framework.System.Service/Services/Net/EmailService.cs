using Framework.System.Service.Contract;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Framework.System.Service.Net
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings emailSettings;
        public EmailService(IOptions<EmailSettings> settings)
        {
            emailSettings = settings.Value;
        }

        public string SendMail(string mailSubject, string messageBody, string mailRecipient)
        {
            if (emailSettings.Enabled == false) return "EmailSettings.Enabled is false";

            var message = new MailMessage()
            {
                IsBodyHtml = true,
                From = new MailAddress(emailSettings.MailFromAddress, emailSettings.DisplayNameFrom),
                Subject = mailSubject,
                Body = messageBody
            };

            message.To.Add(new MailAddress(mailRecipient));
            return TrySendMail(message);
        }


        private string TrySendMail(MailMessage message)
        {
            try
            {
                var smtpClient = new SmtpClient();

                smtpClient.EnableSsl = emailSettings.UseSsl;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(emailSettings.Username, emailSettings.Password);

                if (emailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;
                    smtpClient.EnableSsl = false;

                    message.BodyEncoding = Encoding.Default;
                }
                smtpClient.Send(message);
                return string.Empty;
            }
            catch (SmtpFailedRecipientException ex)
            {
                return "Ваш почтовый ящик недоступен";
            }
            catch (Exception ex)
            {
                return "Сервис временно недоступен. Пожалуйста повторите попытку позже";
            }
        }
    }
}
