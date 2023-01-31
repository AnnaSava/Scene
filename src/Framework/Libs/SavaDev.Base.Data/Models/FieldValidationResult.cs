using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Data.Models
{
    public class FieldValidationResult
    {
        public string Name { get; }

        public bool IsValid { get; set; }

        public List<string> Messages { get; set; } = new List<string>();

        public FieldValidationResult(string name) { Name = name; }

        public FieldValidationResult(string name, bool isValid, string message)
        {
            Name = name;
            IsValid = isValid;
            Messages.Add(message);
        }
    }
}
