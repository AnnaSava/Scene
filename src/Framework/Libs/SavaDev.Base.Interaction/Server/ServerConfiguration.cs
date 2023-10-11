using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Interaction.Server
{
    public class ServerConfiguration
    {
        public string Host { get; set; }

        public int Port { get; set; }

        public int ConnectionsCount { get; set; }

        public int MaxDataSize { get; set; }
    }
}
