using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Infrastructure.Util.ModelFieldsManager
{
    public class ModelFieldsSqlWriterOptions
    {
        public int RowsCountNeeded { get; set; }

        public bool UseMethodNameColumn { get; private set; }

        public string SqlConnectionString { get; set; } = "Server=DESKTOP-PJFB6BC;Database=SavaDevTestData;Trusted_Connection=True;";

        public ModelFieldsSqlWriterOptions(bool useMethodNameColumn)
        {
            UseMethodNameColumn = useMethodNameColumn;
        }
    }
}
