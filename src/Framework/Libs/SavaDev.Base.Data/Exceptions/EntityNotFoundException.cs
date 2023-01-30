using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Data.Exceptions
{
    public class EntityNotFoundException : DataException
    {
        public EntityNotFoundException() { }

        public EntityNotFoundException(string message) : base(message) { }

        public EntityNotFoundException(string entityName, string id) : base($"Entity {entityName} with id {id} not found.") { }
    }
}
