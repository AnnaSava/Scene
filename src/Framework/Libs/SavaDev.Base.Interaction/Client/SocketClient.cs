using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace SavaDev.Base.Interaction.Client
{
    public class SocketClient : IDisposable
    {
        private readonly Socket _client;
        private readonly IPEndPoint _ipEndPoint;

        public SocketClient(string host, int port)
        {           
            var ipAddress = Dns.GetHostAddresses(host);
            _ipEndPoint = new IPEndPoint(ipAddress[0], port);

            _client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            _client.Connect(_ipEndPoint);
        }

        public T Send<T>(object query)
        {
            var json = JsonSerializer.Serialize(query);
            byte[] bytes0 = Encoding.ASCII.GetBytes(json);
            var sw = new Stopwatch();
            sw.Start();
            if(!_client.Connected)
            {
                _client.Connect(_ipEndPoint);
            }
            _client.Send(bytes0);

            var responseData = new byte[256];
            var sb = new StringBuilder();
            int bytes;

            do
            {
                bytes = _client.Receive(responseData, responseData.Length, 0);
                sb.Append(Encoding.Unicode.GetString(responseData, 0, bytes));
            }
            while (_client.Available > 0);
            sw.Stop();
            var t = sw.ElapsedMilliseconds;

            var str = sb.ToString();
            var model = JsonSerializer.Deserialize<T>(str);

            _client.Shutdown(SocketShutdown.Both);
            _client.Close();

            return model;
        }

        public void Dispose()
        {
            if(_client.Connected)
            {
                _client.Shutdown(SocketShutdown.Both);
                _client.Close();
            }
            _client.Dispose();
        }
    }
}
