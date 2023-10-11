using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Interaction.Server
{
    public interface IServiceContainer
    {
        object GetService(string serviceName);
    }
}
