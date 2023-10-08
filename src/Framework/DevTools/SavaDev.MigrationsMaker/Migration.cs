using SavaDev.Base.Unit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SavaDev.MigrationsMaker
{
    internal class Migration
    {
        private readonly StartupProject _migrationsAssembly;
        private readonly MigrationProject _targetAssembly;
        private readonly ServiceInfo _service;

        public Migration(StartupProject migrationsAssembly, MigrationProject targetAssembly, ServiceInfo service)
        {
            _migrationsAssembly = migrationsAssembly;
            _targetAssembly = targetAssembly;
            _service = service;
        }

        public void AddMigration(string name, bool modelHasChangesOnly)
        {
            // TODO
            // Checking for pending model changes
            // This feature was added in EF Core 8.0
            // dotnet ef migrations has-pending - model - changes
            // https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/managing?tabs=dotnet-core-cli#checking-for-pending-model-changes

            var snapshotPath = Path.Combine(_targetAssembly.Path, _service.Name, $"{_service.DbContextName}ModelSnapshot.cs");
            var snapshot = modelHasChangesOnly ? File.ReadAllText(snapshotPath) : null;

            var exitCode = AddMigration(name);
            
            if (exitCode == 0)
            {
                if (modelHasChangesOnly)
                {
                    var newSnapshot = File.ReadAllText(snapshotPath);
                    var equals = snapshot == newSnapshot;
                    if (equals)
                    {
                        RemoveMigration();
                    }
                }
            }
        }

        public void ResetMigrations(string initialName)
        {
            ResetDatabase();

            var path = Path.Combine(_targetAssembly.Path, _service.Name);
            var migFiles = Directory.GetFiles(path);

            var migrations = new List<string>();

            foreach (var file in migFiles)
            {
                if (file.EndsWith(".Designer.cs") || file.EndsWith("ModelSnapshot.cs"))
                    continue;
                var fileName = Path.GetFileNameWithoutExtension(file);
                var designerPath = Path.Combine(path, $"{fileName}.Designer.cs");
                if (!File.Exists(designerPath))
                    continue;
                migrations.Add(fileName);
            }

            for (var i = 0; i < migrations.Count; i++)
            {
                RemoveMigration();
            }

            AddMigration(initialName);
        }

        private int AddMigration(string name)
        {
            var args = @$"ef --startup-project {_migrationsAssembly.Path} migrations add {name} --context {_service.DbContextName} --output-dir {_service.Name}";
            var exitCode = StartProcess(args);
            return exitCode;
        }

        private void RemoveMigration()
        {              
            var args = $@"ef --startup-project {_migrationsAssembly.Path} migrations remove --context {_service.DbContextName}";
            StartProcess(args);
        }

        private void ResetDatabase()
        {
            var args = $@"ef --startup-project {_migrationsAssembly.Path} database update 0 --context {_service.DbContextName}";
            StartProcess(args);
        }

        private int StartProcess(string args)
        {
            var startInfo = new ProcessStartInfo();
            startInfo.FileName = "dotnet";
            startInfo.Arguments = args;

            Console.WriteLine(startInfo.Arguments);

            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardInput = true;
            startInfo.WorkingDirectory = _targetAssembly.Path;

            var process = new Process();
            process.StartInfo = startInfo;
            process.Start();
            process.StandardInput.WriteLine(".");
            process.WaitForExit();
            var code = process.ExitCode;
            process.Close();
            return code;
        }
    }
}
