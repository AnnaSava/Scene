using Framework.Base.Types.ModelTypes;
using Framework.Mailer;
using Framework.MailTemplate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Mail.Services
{
    public class MailService : IMailService
    {
        private readonly IMailTemplateService _mailTemplateService;
        private readonly IEmailClient _emailClient;

        public MailService(IMailTemplateService mailTemplateService, IEmailClient emailClient)
        {
            _mailTemplateService = mailTemplateService;
            _emailClient = emailClient;
        }

        public async Task FormatAndSendEmail(MailDataReceivedModel data) //Сделать свой тип данных для уменьшения связности?
        {
            var mail = await _mailTemplateService.FormatMail(data.Action, data.Culture, data.Variables.ToDictionary(v => v.Name, v => v.Value));

            await _emailClient.SendEmail(data.Email, mail.Title, mail.Body);
        }
    }
}
