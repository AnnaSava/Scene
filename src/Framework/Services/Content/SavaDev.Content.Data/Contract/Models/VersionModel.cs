using Savadev.Content.Data.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savadev.Content.Data
{
    public class VersionModel<T> : BaseContentModel<T>
    {
        public string ContentId { get; set; }
    }

    public class VersionModel : VersionModel<string>
    {

    }
}
