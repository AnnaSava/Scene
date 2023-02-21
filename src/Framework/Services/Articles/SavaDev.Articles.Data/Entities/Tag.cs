using SavaDev.Base.Data.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sava.Articles.Data.Entities
{
    public class Tag : IEntity<long>
    {
        public long Id { get; set; }

        public string NormalizedTag { get; set; }

        public int ArticlesCount { get; set; }

        public byte TextCase { get; set; }

        public string Section { get; set; }

        public virtual ICollection<ArticleTag> Articles { get; set; }
    }
}
