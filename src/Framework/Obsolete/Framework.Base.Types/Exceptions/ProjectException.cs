using System;

namespace Framework.Base.Exceptions
{
    public class ProjectException : Exception
    {
        public ProjectException(string message)
            : base(message)
        {
        }
    }
}
