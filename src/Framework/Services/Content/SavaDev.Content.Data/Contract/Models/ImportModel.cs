using SavaDev.Content.Data.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Content.Data
{
    public class ImportModel<T> : BaseContentModel<T>
    { 
        public string? ContentId { get; set; }

    }

    public class ImportModel : ImportModel<string>
    {

    }
}
