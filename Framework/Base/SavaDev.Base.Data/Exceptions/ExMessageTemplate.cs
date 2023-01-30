using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Data.Exceptions
{
    public static class ExMessageTemplate
    {
        // TODO проверить работу атрибута NotNull и, возможно, это сообщение не понадобится
        public static string ConstructorNullArgument(string argName, string argTypeName, string serviceTypeNname)
             => $"Argument {argName} of type {argTypeName} in constructor of type {serviceTypeNname} is null.";
    }
}
