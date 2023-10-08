using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.MigrationsMaker
{
    internal class MigrationsExecutor
    {
        private readonly StartupProject _startupProject;
        private readonly IEnumerable<MigrationProject> _migrationProjects;
        private readonly MigrationsProfile _profile;
        public MigrationsExecutor(StartupProject startupProject, IEnumerable<MigrationProject> migrationProjects, MigrationsProfile profile)
        {
            _startupProject = startupProject ?? throw new ArgumentNullException(nameof(startupProject));
            _migrationProjects = migrationProjects ?? throw new ArgumentNullException(nameof(migrationProjects));
            _profile = profile ?? throw new ArgumentNullException(nameof(profile));
        }

        public void Run()
        {
            foreach (var migrationProject in _migrationProjects)
            {
                _startupProject.SetProvider(migrationProject.ProviderName);
                foreach (var service in _profile.Services)
                {
                    var migration = new Migration(_startupProject, migrationProject, service);
                    if (_profile.Reset)
                    {
                        migration.ResetMigrations(_profile.MigrationsName);
                    }
                    else
                    {
                        migration.AddMigration(_profile.MigrationsName, _profile.ModelHasChangesOnly);
                    }
                }
            }
            _startupProject.ResetProvider();
        }
    }
}
