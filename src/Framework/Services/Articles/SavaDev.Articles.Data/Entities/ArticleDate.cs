using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sava.Articles.Data.Entities
{
    public class ArticleDate
    {
        [Key]
        public long ArticleId { get; set; }

        public virtual Article Article { get; set; }

        [Key]
        public DateTime Date { get; set; }
    }
}
