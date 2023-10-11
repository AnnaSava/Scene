using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace SavaDev.Base.Interaction.Server
{
    public class ServerQuery
    {
        public string Service { get; }

        public string Action { get; }

        public JObject Data { get; }

        public ServerQuery(string jsonString)
        {
            var query = JObject.Parse(jsonString);
            Service = query["Service"].ToString();
            Action = query["Action"].ToString();
            Data = (JObject)query["Data"];
        }
    }

    public class ServerQuery<T> where T : class
    {
        public string Service { get; set; }

        public string Action { get; set; }

        public T Data { get; set; }
    }
}
