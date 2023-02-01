using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Infrastructure.Util.ModelFieldsManager
{
    public class ModelFieldsCsvWriterOptions
    {
        public string OutputFolderName { get; set; } = "Output";

        public int RowsCountToGenerate { get; set; }

        public bool InsertTestNameColumn { get; set; }

        public char CsvSeparator { get; set; } = ';';

        public bool UseMethodNameColumn { get; set; }

        public ModelFieldsCsvWriterOptions(bool useMethodNameColumn)
        {
            UseMethodNameColumn = useMethodNameColumn;
        }
    }
}
