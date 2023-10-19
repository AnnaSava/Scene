using SavaDev.Base.Data.Registry;
using SavaDev.Base.Front.Registry;
using SavaDev.Base.Front.Services;
using SavaDev.Content.Contract;
using SavaDev.Content.Contract.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using SavaDev.Content.Front.Contract.Models;
using SavaDev.Base.Interaction.Server;
using SavaDev.Base.Front.Models;

namespace SavaDev.Content.Client.Services
{
    public class DraftClientService : IDraftFrontService
    {
        public async Task<RegistryPageViewModel<DraftViewModel>> GetRegistryPage(RegistryQuery query)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceCheckOk> Check(ServiceCheckQuery query)
        {
            var serverQuery = new ServerQuery<ServiceCheckQuery>()
            {
                Service = "Draft",
                Action = nameof(Check),
                Data = query
            };
            var result = Send<ServiceCheckOk>(serverQuery);
            return result;
        }

        public async Task<OperationViewResult> Create(DraftViewModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<GetFormViewResult> GetForm(GetFormViewQuery query)
        {
            var serverQuery = new ServerQuery<GetFormViewQuery>()
            {
                Service = "Draft",
                Action = "GetForm",
                Data = query
            };

            var result = Send<GetFormViewResult>(serverQuery);
            return result;
        }

        public T Send<T>(object query)
        {
            var ipAddress = Dns.GetHostAddresses("localhost");
            var ipEndPoint = new IPEndPoint(ipAddress[0], 5757);

            var client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            client.Connect(ipEndPoint);

            var json = JsonSerializer.Serialize(query);
            byte[] bytes0 = Encoding.ASCII.GetBytes(json);
            var sw = new Stopwatch();
            sw.Start();
            client.Send(bytes0);

            var responseData = new byte[256];
            var sb = new StringBuilder();
            int bytes;

            do
            {
                bytes = client.Receive(responseData, responseData.Length, 0);
                sb.Append(Encoding.Unicode.GetString(responseData, 0, bytes));
            }
            while (client.Available > 0);
            sw.Stop();
            var t = sw.ElapsedMilliseconds;

            var str = sb.ToString();
            var model = JsonSerializer.Deserialize<T>(str);

            client.Shutdown(SocketShutdown.Both);
            client.Close();

            return model;
        }
    }
}
