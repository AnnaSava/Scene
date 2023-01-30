using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Boxyz.Data.Entities
{
    public class Board : BaseEditionEntity<UniBoard>
    {
        public long UniqId { get; set; }

        public virtual UniBoard Uniq { get; set; }

        public virtual ICollection<Shape> Shapes { get; set; }

        public virtual ICollection<BoardFolk> Folks { get; set; }
    }
}
