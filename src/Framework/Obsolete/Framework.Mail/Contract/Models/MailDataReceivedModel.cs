using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Mail
{
    public class MailDataReceivedModel
    {
        public string Email { get; set; }

        public string Action { get; set; }

        public string Culture { get; set; }

        public IEnumerable<MailVariableReceivedModel> Variables { get; set; }
    }
}
