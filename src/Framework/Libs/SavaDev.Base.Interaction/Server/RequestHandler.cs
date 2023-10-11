using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SavaDev.Base.Interaction.Server
{
    internal class RequestHandler
    {
        private readonly IServiceContainer _container;

        public RequestHandler(IServiceContainer container)
        {
            _container = container;
        }

        public async Task<byte[]> Invoke(byte[] data)
        {
            var str = Encoding.Default.GetString(data);
            var result = await Invoke(str);
            var resultData = Encoding.Unicode.GetBytes(result);
            return resultData;
        }

        public async Task<string> Invoke(string jsonQuery)
        {
            var query = new ServerQuery(jsonQuery);
            var result = await Invoke(query);
            var msg = JsonSerializer.Serialize(result);
            return msg;
        }

        public async Task<object?> Invoke(ServerQuery query)
        {
            var service = _container.GetService(query.Service);

            var method = service.GetType().GetMethod(query.Action);
            var pars = method.GetParameters();
            var vals = new object[pars.Length];
            for (int i = 0; i < pars.Length; i++)
            {
                var data = query.Data.ToObject(pars[i].ParameterType); // TODO исправить чтение параметров
                vals[i] = data;
            }
            dynamic task = method.Invoke(service, vals);
            var result = await task;
            return result;
        }
    }
}
