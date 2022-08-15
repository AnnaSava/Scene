using Framework.Mail;
using Framework.Mail.Worker;
using Framework.Mailer;
using Framework.MailTemplate;
using Framework.Worker.Register;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Text.Json;

IConfigurationRoot config = GetConfiguration();

string WorkerName = Assembly.GetExecutingAssembly().FullName;
string HostName = config.GetSection("RabbitMq:HostName").Value;
const string QueueName = "mailsdata";

Console.WriteLine(WorkerName);

IServiceCollection services = new ServiceCollection();
services.Configure<EmailConfiguration>(config.GetSection("Email"));

services.AddMapper();

services.AddMailTemplate(config.GetConnectionString("IdentityConnection"), "Scene.Migrations.PostgreSql", config);

var work = new Work(HostName, QueueName);
work.Execute(DoAction, services);

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();

static async void DoAction(string message, IServiceCollection services)
{
    Console.WriteLine(message);
    ServiceProvider serviceProvider = services.BuildServiceProvider(); 
    var mailService = serviceProvider.GetService<IMailService>();

    var data = JsonSerializer.Deserialize<MailDataReceivedModel>(message);

    await mailService.FormatAndSendEmail(data);
}

static IConfigurationRoot GetConfiguration()
{
    string appsettingsPath = Path.GetFullPath(
        Path.Combine(AppContext.BaseDirectory, "appsettings.json"));

    return new ConfigurationBuilder()
        .AddJsonFile(appsettingsPath, true, true)
        .Build();
}