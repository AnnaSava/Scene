using SavaDev.Base.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.MigrationsMaker
{
    internal class Solution
    {
        public const string FrameworkName = "SavaDev";

        public string SolutionName { get; } = string.Empty;

        public string SolutionPath { get; } = string.Empty;

        public string FilePath => Path.Combine(SolutionPath, $"{SolutionName}.sln");

        public string FrameworkServicesRelativePath => @"Framework\Services";

        public string FrameworkServicesPath => Path.Combine(SolutionPath, FrameworkServicesRelativePath);

        public string ServicesRelativePath => "Services";

        public string ServicesPath => Path.Combine(SolutionPath, ServicesRelativePath);

        public string MigrationsPath => Path.Combine(SolutionPath, "Migrations");

        public Solution(string solutionName, string solutionPath)
        {
            SolutionName = solutionName;
            SolutionPath = GetSolutionRootFolder(solutionPath);
        }

        private string GetSolutionRootFolder(string solutionPath)
        {
            if (!string.IsNullOrEmpty(solutionPath))
                return solutionPath;
            var defaultFolder = @"..\..\..\..\..\..\";
            return Path.GetFullPath(defaultFolder);
        }

        private string GetSolutionText()
        {
            var solutionText = File.ReadAllText(FilePath);
            return solutionText;
        }

        public List<ServiceInfo> GetSolutionServices()
        {
            var solutionText = GetSolutionText();
            var solutionServices = new List<ServiceInfo>();
            solutionServices.AddRange(GetSolutionServices(FrameworkServicesRelativePath, FrameworkName, solutionText));
            solutionServices.AddRange(GetSolutionServices(ServicesRelativePath, SolutionName, solutionText));
            return solutionServices;
        }

        public List<MigrationProject> GetMigrationProjects()
        {
            var migrationsDirs = Directory.GetDirectories(MigrationsPath);
            var projects = new List<MigrationProject>();

            foreach(var dir in migrationsDirs)
            {
                var projectName = new DirectoryInfo(dir).Name;
                if (projectName.StartsWith(SolutionName))
                {
                    projects.Add(new MigrationProject(projectName, MigrationsPath));
                }
            }

            return projects;
        }

        List<ServiceInfo> GetSolutionServices(string servicesRelativePath, string projectPrefix, string solutionText)
        {
            var servicesPath = Path.Combine(SolutionPath, servicesRelativePath);
            var servicesDirs = Directory.GetDirectories(servicesPath);

            var solutionServices = new List<ServiceInfo>();

            foreach (var serviceDir in servicesDirs)
            {
                var serviceName = new DirectoryInfo(serviceDir).Name;
                var dataProjectRelativePath = string.Format(@"{1}\{0}\{2}.{0}.Data\{2}.{0}.Data.csproj", serviceName, servicesRelativePath, projectPrefix);
                if (solutionText.Contains(dataProjectRelativePath))
                {
                    solutionServices.Add(new ServiceInfo(serviceName, serviceDir));
                }
            }

            return solutionServices;
        }
    }
}
