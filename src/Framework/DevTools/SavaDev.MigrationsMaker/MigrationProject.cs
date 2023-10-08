using SavaDev.Base.Unit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.MigrationsMaker
{
    internal class MigrationProject
    {
        public string Name { get; } = string.Empty;

        public string Path { get; } = string.Empty;

        public string ProviderName { get; } = string.Empty;

        private readonly Dictionary<string, string> DbProviders = new Dictionary<string, string>
        {
            { "PostgreSql", DbProviderName.DefaultPgName },
            { "MsSql", DbProviderName.DefaultMsName }
        };

        public MigrationProject(string name, string migrationsPath)
        {
            Name = name;
            Path = System.IO.Path.Combine(migrationsPath, name);
            var postfix = Name.Substring(Name.LastIndexOf('.') + 1);
            ProviderName = DbProviders[postfix];
        }
    }
}
