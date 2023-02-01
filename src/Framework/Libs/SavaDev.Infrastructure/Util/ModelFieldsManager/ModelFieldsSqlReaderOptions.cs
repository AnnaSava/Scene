using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Infrastructure.Util.ModelFieldsManager
{
    public class ModelFieldsSqlReaderOptions
    {
        public bool UseMethodNameColumn { get; set; }

        public string SqlConnectionString { get; set; } = "Server=DESKTOP-PJFB6BC;Database=SavaDevTestData;Trusted_Connection=True;";

        public ModelFieldsSqlReaderOptions(bool useMethodNameColumn)
        {
            UseMethodNameColumn = useMethodNameColumn;
        }
    }
}
