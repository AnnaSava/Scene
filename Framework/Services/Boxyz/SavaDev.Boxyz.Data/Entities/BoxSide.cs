using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Boxyz.Data.Entities
{
    public class BoxSide : BaseEntity<Guid>
    {
        public Guid BoxId { get; set; }

        public virtual Box Box { get; set; }

        public Guid ShapeSideId { get; set; }

        public virtual ShapeSide ShapeSide { get; set; }

        public bool IsCollection { get; set; }

        public string ValueJson { get; set; }

        public virtual ICollection<ValueSimpleText> SimpleTexts { get; set; }

        public virtual ICollection<ValueText> Texts { get; set; }

        public virtual ICollection<ValueFloat> Floats { get; set; }

        public virtual ICollection<ValueInteger> Integers { get; set; }

        public virtual ICollection<ValueCoin> Coins { get; set; }

        public virtual ICollection<Value3dPoint> Points3d { get; set; }

        public virtual ICollection<Value2dPoint> Points2d { get; set; }

        public virtual ICollection<ValueDateTime> Dates { get; set; }

        public virtual ICollection<ValueLink> Links { get; set; }

        public virtual ICollection<ValueImage> Images { get; set; }

        public virtual ICollection<ValueFile> Files { get; set; }
    }
}
