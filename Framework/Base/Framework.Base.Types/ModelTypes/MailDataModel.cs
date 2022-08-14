using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Base.Types.ModelTypes
{
    // TODO подумать, где лучше разместить этот класс
    public class MailDataModel
    {
        public string Email { get; set; }

        public string TemplatePermName { get; set; }

        public string Culture { get; set; }

        public IEnumerable<MailVariableModel> Variables { get; set; }
    }
}
