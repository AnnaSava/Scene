using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Base.Service.ListView
{
    [Obsolete]
    public class ListFilterViewModel
    {
        public string Id { get; set; }

        public bool IsDeleted { get; set; }
    }

    [Obsolete]
    public class SimpleFilterViewModel 
    {
        public string SearchText { get; set; }
    }
}
