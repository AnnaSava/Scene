using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace SavaDev.Base.Interaction.Server
{
    public class SocketServer
    {
        private readonly Socket _socket;
        private readonly ServerConfiguration _settings;
        private readonly RequestHandler _handler;
        private readonly ILogger _logger;

        public SocketServer(ServerConfiguration settings, IServiceContainer container, ILogger logger)
        {
            var ipEndPoint = new IPEndPoint(IPAddress.Any, settings.Port);
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _socket.Bind(ipEndPoint);
            _settings = settings;
            _handler = new RequestHandler(container); // TODO возможно, сделать через DI
            _logger = logger;
        }

        public async Task StartServer()
        {
            _logger.LogInformation($"host {_settings.Host} port {_settings.Port}");

            while (true)
            {
                try
                {
                    _socket.Listen(_settings.ConnectionsCount);
                    using var client = _socket.Accept();

                    byte[] clientData = new byte[1024 * _settings.MaxDataSize];
                    int receivedBytes = client.Receive(clientData);
                    var arr = clientData.Take(receivedBytes).ToArray();
                    _logger.LogInformation("Data received");
                    try
                    {
                        var data = await _handler.Invoke(arr);
                        client.Send(data);
                        _logger.LogInformation("Data sent");
                    }
                    catch (Exception exInvoke)
                    {
                        var msg = GetErrorData(exInvoke.Message);
                        client.Send(msg);
                        _logger.LogError("\t" + exInvoke.Message);
                    }

                    client.Shutdown(SocketShutdown.Both);
                    client.Close();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }
        }

        private byte[] GetErrorData(string errMessage)
        {
            var msg = Encoding.Unicode.GetBytes("{\"Success\":false, \"ErrMessage\": \"" + errMessage + "\"}");
            return msg;
        }
    }
}
