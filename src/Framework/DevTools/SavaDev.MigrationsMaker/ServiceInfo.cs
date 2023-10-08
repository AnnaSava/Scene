using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.MigrationsMaker
{
    internal class ServiceInfo
    {
        public string Name { get; } = string.Empty;

        public string Path { get; } = string.Empty;

        public string DbContextName => $"{Name}Context";

        public ServiceInfo(string name, string path) 
        {
            Name = name;
            Path = path;
        }
    }
}
