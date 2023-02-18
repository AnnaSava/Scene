using Microsoft.Extensions.Options;
using SavaDev.Mail.Service.Contract;
using SavaDev.Mail.Service.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Mail.Service.Services
{
    public class EmailClient : IEmailClient
    {
        private readonly SmtpClient _smtpClient;
        private readonly EmailConfiguration _options;

        public EmailClient(
            SmtpClient smtpClient,
            IOptions<EmailConfiguration> options)
        {
            _smtpClient = smtpClient;
            _options = options.Value;

            smtpClient.EnableSsl = _options.UseSsl;
            smtpClient.Host = _options.ServerName;
            smtpClient.Port = _options.ServerPort;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(_options.Username, _options.Password);

            if (_options.WriteAsFile)
            {
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                smtpClient.PickupDirectoryLocation = _options.FileLocation;
                smtpClient.EnableSsl = false;
            }
        }

        public async Task<string> SendEmail(string email, string subject, string body)
        {
            // TODO подумать, что тут лучше возвращать
            try
            {
                var message = new MailMessage()
                {
                    IsBodyHtml = true,
                    From = new MailAddress(_options.MailFromAddress, _options.DisplayNameFrom),
                    Subject = subject,
                    Body = body
                };

                message.To.Add(new MailAddress(email));

                if (_options.WriteAsFile)
                {
                    message.BodyEncoding = Encoding.Default;
                }

                await _smtpClient.SendMailAsync(message);
                return "ok";
            }
            catch (SmtpFailedRecipientException e)
            {
                //TODO logger.InfoException(String.Format("Почтовый ящик {0} не доступен", message.To.FirstOrDefault().Address), e);
                return "Ваш почтовый ящик недоступен";
            }
            catch
            {
                // TODO logger.InfoException("Сервис временно недоступен", e);
                return "Сервис временно недоступен. Пожалуйста повторите попытку позже";
            }
        }
    }
}
