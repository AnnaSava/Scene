using SavaDev.Base.Data.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savadev.Content.Data.Contract.Models
{
    public abstract class BaseContentModel<T> : IFormModel
    {
        public Guid Id { get; set; }

        public string Entity { get; set; }

        public string Module { get; set; }

        public string OwnerId { get; set; }

        public T Content { get; set; }

        public DateTime Created { get; set; }
    }
}
