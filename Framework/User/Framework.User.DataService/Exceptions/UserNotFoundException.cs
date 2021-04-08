using Framework.Base.Exceptions;
using System;

namespace Framework.User.DataService.Exceptions
{
   public class UserNotFoundException : ProjectException
    {
        private const string ExceptionMessageTemplate = "Type: {0}. Method: {1}. Argument: {2}. Value: {3}. Message: {4}";

        public UserNotFoundException()
        {
        }

        public UserNotFoundException(string message)
            : base(message) { }

        public UserNotFoundException(Type errorLocationType, string methodName, long id)
            : base(string.Format(ExceptionMessageTemplate, errorLocationType.FullName, methodName, id)) { }

        public UserNotFoundException(Type errorLocationType, string methodName, string name)
            : base(string.Format(ExceptionMessageTemplate, errorLocationType.FullName, methodName, name)) { }

        public UserNotFoundException(string message, Exception inner)
            : base(message, inner) { }
    }
}
