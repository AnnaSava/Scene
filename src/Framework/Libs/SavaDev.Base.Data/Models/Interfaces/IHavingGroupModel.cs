using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Data.Models.Interfaces
{
    public interface IHavingGroupModel<TKey>
    {
        public TKey Id { get; set; }

        public string OwnerId { get; set; }

        public Guid? GroupId { get; set; }
    }
}
