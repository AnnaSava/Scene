using SavaDev.Boxyz.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Boxyz.Data.Entities
{
    public interface IFolkEntity
    {
        public Guid ContentId { get; set; }

        public string Culture { get; set; }
    }

    public abstract class BaseFolkEntity<TContent> : IFolkEntity 
        where TContent : BaseEntity<Guid>
    {
        [Key]
        public Guid ContentId { get; set; }

        [Key]
        public string Culture { get; set; }

        public virtual TContent Content { get; set; }
    }
}
