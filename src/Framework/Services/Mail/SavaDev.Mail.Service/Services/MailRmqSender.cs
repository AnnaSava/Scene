using EasyNetQ;
using SavaDev.Mail.Service.Contract;
using SavaDev.Mail.Service.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Mail.Service.Services
{
    public class MailRmqSender : IMailSender
    {
        private readonly string _host = "host=localhost";

        public async Task<MailSendResult> SendInfo(MailDataModel info)
        {
            using (var bus = RabbitHutch.CreateBus(_host))
            {
                var result = await bus.Rpc.RequestAsync<MailDataModel, MailSendResult>(info);
                return result;
            }
        }
    }
}
