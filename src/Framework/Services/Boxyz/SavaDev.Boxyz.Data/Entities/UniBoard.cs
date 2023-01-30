using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Boxyz.Data.Entities
{
    public class UniBoard : BaseUniEntity<Board>
    {
        public long? ParentUniBoardId { get; set; }

        public virtual UniBoard ParentUniBoard { get; set; }

        public virtual ICollection<UniBoard> ChildUniBoards { get; set; }

        public int Level { get; set; }

        public string Path { get; set; }

        public virtual ICollection<UniShape> UniShapes { get; set; }
    }
}
