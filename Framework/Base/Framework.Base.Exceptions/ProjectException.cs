using System;

namespace Framework.Base.Exceptions
{
    public class ProjectException : Exception
    {
        protected ProjectException()
        {
        }

        protected ProjectException(string message)
            : base(message)
        {
        }

        protected ProjectException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
