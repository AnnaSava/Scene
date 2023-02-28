using System;

namespace Framework.Base.Exceptions
{
    public class ProjectArgumentException : ProjectException
    {
        private const string ExceptionMessageTemplate = "Type: {0}. Method: {1}. Argument: {2}. Value: {3}. Message: {4}";

        public ProjectArgumentException(
            Type errorLocationType,
            string methodName,
            string argumentName,
            object value,
            string message = "")
            : base(string.Format(ExceptionMessageTemplate, errorLocationType.FullName, methodName, argumentName, value, message))
        {
        }
    }
}
