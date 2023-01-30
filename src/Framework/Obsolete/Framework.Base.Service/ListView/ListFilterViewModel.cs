using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Base.Service.ListView
{
    public class ListFilterViewModel
    {
        public string Id { get; set; }

        public bool IsDeleted { get; set; }
    }

    public class SimpleFilterViewModel 
    {
        public string SearchText { get; set; }
    }
}
