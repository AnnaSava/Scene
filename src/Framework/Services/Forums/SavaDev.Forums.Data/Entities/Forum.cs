using SavaDev.Base.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sava.Forums.Data.Entities
{
    public class Forum : BaseAliasedEntity<long>
    {
        public string Section { get; set; }

        public string Title { get; set; }

        public string Info { get; set; }

        public int Topics { get; set; }

        public int Posts { get; set; }
    }
}
