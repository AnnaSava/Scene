using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Base.Service.ListView
{
    public class ListItemsFilterModel
    {
        public string SearchText { get; set; }

        public bool StartsWith { get; set; }

        public ListItemsFilterModel()
        {
            StartsWith = true;
        }
    }
}
