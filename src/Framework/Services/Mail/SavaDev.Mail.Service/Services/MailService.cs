using SavaDev.Mail.Service.Contract;
using SavaDev.Mail.Service.Contract.Models;
using SavaDev.General.Data.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SavaDev.General.Front.Contract;

namespace SavaDev.Mail.Service.Services
{
    public class MailService : IMailService
    {
        private readonly IMailTemplateViewService _mailTemplateService;
        private readonly IEmailClient _emailClient;

        public MailService(IMailTemplateViewService mailTemplateService, IEmailClient emailClient)
        {
            _mailTemplateService = mailTemplateService;
            _emailClient = emailClient;
        }

        public async Task<MailSendResult> FormatAndSendEmail(MailDataModel data) //Сделать свой тип данных для уменьшения связности?
        {
            var mail = await _mailTemplateService.FormatMail(data.Action, data.Culture, data.Variables.ToDictionary(v => v.Name, v => v.Value));

            await _emailClient.SendEmail(data.Email, mail.Title, mail.Body);
            return new MailSendResult();
        }
    }
}
