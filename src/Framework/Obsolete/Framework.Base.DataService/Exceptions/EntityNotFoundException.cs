using Framework.Base.Exceptions;
using System;

namespace Framework.Base.DataService.Exceptions
{
    [Obsolete]
   public class EntityNotFoundException : ProjectException
    {
        private const string ExceptionMessageTemplate = "Type: {0}. Method: {1}. Argument: {2}. Value: {3}.";

        public EntityNotFoundException()
        {
        }

        public EntityNotFoundException(string message)
            : base(message) { }

        public EntityNotFoundException(Type errorLocationType, string methodName, string entityTypeName, long id)
            : base(string.Format(ExceptionMessageTemplate, errorLocationType.FullName, methodName, entityTypeName, id)) { }

        public EntityNotFoundException(Type errorLocationType, string methodName, string entityTypeName, string name)
            : base(string.Format(ExceptionMessageTemplate, errorLocationType.FullName, methodName, entityTypeName, name)) { }

        public EntityNotFoundException(string message, Exception inner)
            : base(message, inner) { }
    }
}
