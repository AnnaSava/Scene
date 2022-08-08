using Framework.Mailer;
using Framework.Mailer.Services;
using Framework.Worker.Register;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Mail;

const string WorkerName = "Framework.Worker.Register";
const string HostName = "localhost";
const string QueueName = "registrations";

Console.WriteLine(WorkerName);

IConfigurationRoot config = GetConfiguration();
IServiceCollection services = new ServiceCollection();
services.Configure<EmailConfiguration>(config.GetSection("Email"));

services.AddSingleton<SmtpClient>();
services.AddSingleton<IEmailClient, EmailClient>();

var work = new Work(HostName, QueueName);
work.Execute(DoAction, services);

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();

static async void DoAction(string message, IServiceCollection services)
{
    Console.WriteLine(message);
    ServiceProvider serviceProvider = services.BuildServiceProvider(); 
    var emailClient = serviceProvider.GetService<IEmailClient>();
    await emailClient.SendEmail(message, "hello", "world");
}

static IConfigurationRoot GetConfiguration()
{
    string appsettingsPath = Path.GetFullPath(
        Path.Combine(AppContext.BaseDirectory, "appsettings.json"));

    return new ConfigurationBuilder()
        .AddJsonFile(appsettingsPath, true, true)
        .Build();
}