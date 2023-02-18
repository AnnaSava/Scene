using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Mail.Service.Contract
{
    public interface IEmailClient
    {
        Task<string> SendEmail(string email, string subject, string body);
    }
}
