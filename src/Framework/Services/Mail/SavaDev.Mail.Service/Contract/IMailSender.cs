using SavaDev.Mail.Service.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Mail.Service.Contract
{
    public interface IMailSender
    {
        Task<MailSendResult> SendInfo(MailDataModel info);
    }
}
