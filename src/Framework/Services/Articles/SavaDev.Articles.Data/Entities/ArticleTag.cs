using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sava.Articles.Data.Entities
{
   public class ArticleTag
    {
        [Key]
        public long ArticleId { get; set; }

        public virtual Article Article { get; set; }

        [Key]
        public long TagId { get; set; }

        public virtual Tag Tag { get; set; }

        public string TagText { get; set; }
    }
}
