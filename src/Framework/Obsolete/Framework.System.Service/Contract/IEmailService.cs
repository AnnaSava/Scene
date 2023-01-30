using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.System.Service.Contract
{
    public interface IEmailService
    {
        string SendMail(string mailSubject, string messageBody, string mailRecipient);
    }
}
