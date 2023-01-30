using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sava.Articles.Data
{
    public class ArticleDateModel
    {
        public long ArticleId { get; set; }

        public virtual ArticleModel Article { get; set; }

        [Key]
        public DateTime Date { get; set; }
    }
}
