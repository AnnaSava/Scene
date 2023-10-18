// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SavaDev.Base.Unit.Options;
using SavaDev.Base.Unit;
using SavaDev.Content;
using SavaDev.Content.Contract;
using SavaDev.Content.Server;
using SavaDev.Content.Services;
using SavaDev.Infrastructure;
using SavaDev.Infrastructure.Appsettings;
using System;
using SavaDev.Base.Interaction.Server;
using Microsoft.Extensions.Logging;

Console.WriteLine("Hello, World!");

IConfigurationRoot config = ConfigFile.GetConfiguration(ConfigFile.GetEnvironment());
var services = new ServiceCollection();

services.Configure<ServerConfiguration>(
   config.GetSection("Server"));

services.AddSingleton(sp =>
    sp.GetRequiredService<IOptions<ServerConfiguration>>().Value);

services.AddMapper();
services.AddLogging();
services.AddContent(config, new UnitOptions("Cnt", AppSettings.DefaultConnectionStringPattern));
services.AddContentFront();

ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
var logger = loggerFactory.CreateLogger("SavaDev.Content.Server");

using (ServiceProvider sp = services.BuildServiceProvider())
{
    using (IServiceScope scope = sp.GetRequiredService<IServiceScopeFactory>().CreateScope())
    {
        var serverConfig = sp.GetService<ServerConfiguration>();
        var activeServices = new Dictionary<string, object>()
        {
            { "Draft", sp.GetService(typeof(IDraftFrontService)) },
            { "Version", sp.GetService(typeof(IVersionFrontService)) }
        };
        IServiceContainer container = new ServiceContainer(activeServices);
        var server = new SocketServer(serverConfig, container, logger);
        server.StartServer();
    }
}