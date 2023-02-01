using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Infrastructure.Util.ModelFieldsManager
{
    public class ModelFieldsCsvReaderOptions
    {
        public char CsvSeparator { get; set; } = ';';

        public string InputFolderName { get; set; } = "Data";
    }
}
