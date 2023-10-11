using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Interaction.Server
{
    public class ServiceContainer : IServiceContainer
    {
        private readonly Dictionary<string, object> _services;

        public ServiceContainer(Dictionary<string, object> services)
        {
            _services = services;
        }

        public object GetService(string serviceName)
        {
            return _services[serviceName];
        }
    }
}
