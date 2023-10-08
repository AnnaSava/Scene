using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.MigrationsMaker
{
    internal class MigrationsProfile
    {
        private const string InitialMigrationName = "Initial";

        private readonly IEnumerable<ServiceInfo> _solutionServices;
        private ServiceInfo? _singleService = null;
        private bool _all;

        public bool Reset { get; private set; }

        public bool ModelHasChangesOnly { get; }

        public IEnumerable<ServiceInfo> Services => _all
            ? _solutionServices
            : _singleService == null 
                ? new ServiceInfo[] { } 
                : new ServiceInfo[] { _singleService };

        public string MigrationsName { get; private set; } = string.Empty;

        public MigrationsProfile(IEnumerable<string> args, IEnumerable<ServiceInfo> solutionServices, bool modelHasChangesOnly = false)
        {
            _solutionServices = solutionServices ?? throw new ArgumentNullException(nameof(solutionServices));
            ModelHasChangesOnly = modelHasChangesOnly;

            ProcessArgs(args);            
            if (!CheckProfile())
                throw new Exception("Incorrect command");
        }

        private void ProcessArgs(IEnumerable<string> args)
        {
            foreach (var el in args)
            {
                switch (el.ToLower())
                {
                    case "all":
                        _all = true;
                        break;
                    case "reset":
                        Reset = true;
                        break;
                    default:
                        if (_singleService != null)
                        {
                            MigrationsName = el;
                        }
                        else
                        {
                            _singleService = _solutionServices.FirstOrDefault(s => s.Name.Equals(el, StringComparison.InvariantCultureIgnoreCase));
                            if(_singleService == null)
                            {
                                MigrationsName = el;
                            }
                        }
                        break;
                }
            }

            MigrationsName = GetOrCreateMigrationsName();
        }

        private bool CheckProfile()
        {
            if (_all && _singleService != null)
                return false;
            if (!_all && _singleService == null)
                return false;
            return true;
        }

        private string GetOrCreateMigrationsName()
        {
            if (!string.IsNullOrEmpty(MigrationsName)) return MigrationsName;
            if (Reset) return InitialMigrationName;
            return $"Auto_{Guid.NewGuid().ToString("N")}";
        }
    }
}
