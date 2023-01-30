using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Data.Context
{
    public static class StringExtentions
    {
        public static string ToSnakeCase(this string inputString)
        {
            if (inputString == null)
            {
                throw new ArgumentNullException(nameof(inputString));
            }
            if (inputString.Length < 2)
            {
                return inputString;
            }
            var sb = new StringBuilder();
            sb.Append(char.ToLowerInvariant(inputString[0]));
            for (int i = 1; i < inputString.Length; ++i)
            {
                char c = inputString[i];
                if (char.IsUpper(c))
                {
                    sb.Append('_');
                    sb.Append(char.ToLowerInvariant(c));
                }
                else
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
    }
}
