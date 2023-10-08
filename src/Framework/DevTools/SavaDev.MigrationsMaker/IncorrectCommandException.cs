using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("SavaDev.DevTools.Tests")]
namespace SavaDev.MigrationsMaker
{
    internal class IncorrectCommandException : Exception
    {
        public IncorrectCommandException() : base("Incorrect command") { }
    }
}
