using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Base.Types.ModelTypes
{
    public class MailDataModel
    {
        public string Email { get; set; }

        public string Action { get; set; }

        public string Culture { get; set; }

        public IEnumerable<MailVariableModel> Variables { get; set; }
    }
}
