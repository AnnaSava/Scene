using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Data.Exceptions
{
    public class EntityAlreadyExistsException : DataException
    {
        public EntityAlreadyExistsException() { }

        public EntityAlreadyExistsException(string message) : base(message) { }

        public EntityAlreadyExistsException(string entityName, string id) : base($"Entity {entityName} with id {id} already exists.") { }
    }
}
