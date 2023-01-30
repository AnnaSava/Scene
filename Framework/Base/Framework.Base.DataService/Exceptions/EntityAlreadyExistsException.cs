using Framework.Base.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Base.DataService.Exceptions
{
    [Obsolete]
    public class EntityAlreadyExistsException : ProjectException
    {
        private const string ExceptionMessageTemplate = "Type: {0}. Method: {1}. Entity: {2}. Field: {3}. Value: {4}";

        public EntityAlreadyExistsException()
        {
        }

        public EntityAlreadyExistsException(string message)
            : base(message) { }

        public EntityAlreadyExistsException(Type errorLocationType, string methodName, string entityTypeName, long id)
            : base(string.Format(ExceptionMessageTemplate, errorLocationType.FullName, methodName, entityTypeName, id)) { }

        public EntityAlreadyExistsException(Type errorLocationType, string methodName, Type entityType, string fieldName, string value)
            : base(string.Format(ExceptionMessageTemplate, errorLocationType.FullName, methodName, entityType.FullName, fieldName, value)) { }

        public EntityAlreadyExistsException(string message, Exception inner)
            : base(message, inner) { }
    }
}
