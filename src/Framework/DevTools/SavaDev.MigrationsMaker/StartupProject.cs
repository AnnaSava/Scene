using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SavaDev.MigrationsMaker
{
    internal class StartupProject
    {
        private readonly string _backupDbProvider;

        public string AppsettingsPath { get; } = string.Empty;

        public string RelativePath { get; } = string.Empty;

        public string Path { get; } = string.Empty;

        public string Name { get; } = string.Empty;

        public StartupProject(string relativePath, string solutionPath)
        {
            RelativePath = relativePath;
            Path = System.IO.Path.Combine(solutionPath, relativePath);
            Name = new DirectoryInfo(Path).Name;
            AppsettingsPath = System.IO.Path.Combine(Path, "appsettings.json");
            _backupDbProvider = GetProvider();
        }

        public string GetProvider()
        {
            var json = JObject.Parse(File.ReadAllText(AppsettingsPath));
            var provider = (string)json["DbProvider"];
            return provider;
        }

        public void SetProvider(string providerCode)
        {
            var json = JObject.Parse(File.ReadAllText(AppsettingsPath));
            json["DbProvider"] = providerCode;
            File.WriteAllText(AppsettingsPath, json.ToString());
        }

        public void ResetProvider()
        {
            SetProvider(_backupDbProvider);
        }
    }
}
