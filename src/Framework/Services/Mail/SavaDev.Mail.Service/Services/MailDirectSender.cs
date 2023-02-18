using SavaDev.Mail.Service.Contract;
using SavaDev.Mail.Service.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Mail.Service.Services
{
    public class MailDirectSender : IMailSender
    {
        private readonly IMailService _mailService;

        public MailDirectSender(IMailService mailService)
        {
            _mailService = mailService;
        }

        public async Task<MailSendResult> SendInfo(MailDataModel info)
        {
            var resultModel = await _mailService.FormatAndSendEmail(info);

            return resultModel;
        }
    }
}
