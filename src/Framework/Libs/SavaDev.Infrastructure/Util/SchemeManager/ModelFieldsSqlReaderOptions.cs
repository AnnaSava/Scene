using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Infrastructure.Util.TestDataGenerator
{
    public class ModelFieldsSqlReaderOptions
    {
        public bool UseMethodNameColumn { get; set; }

        public string SqlConnectionString { get; set; }

        public ModelFieldsSqlReaderOptions(string sqlConnection, bool useMethodNameColumn)
        {
            SqlConnectionString = sqlConnection;
            UseMethodNameColumn = useMethodNameColumn;
        }
    }
}
