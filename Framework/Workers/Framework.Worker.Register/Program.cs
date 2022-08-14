using AutoMapper;
using Framework.Base.Types.ModelTypes;
using Framework.Mailer;
using Framework.Mailer.Services;
using Framework.MailTemplate;
using Framework.MailTemplate.Data.Mapper;
using Framework.MailTemplate.Service.Mapper;
using Framework.MailTemplate.Services;
using Framework.Worker.Register;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Mail;
using System.Text.Json;

const string WorkerName = "Framework.Worker.Register";
const string HostName = "localhost";
const string QueueName = "registrations";

Console.WriteLine(WorkerName);

IConfigurationRoot config = GetConfiguration();
IServiceCollection services = new ServiceCollection();
services.Configure<EmailConfiguration>(config.GetSection("Email"));

var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MailTemplateDataMapperProfile());
    mc.AddProfile(new MailTemplateMapperProfile());
});

IMapper mapper = mappingConfig.CreateMapper();
services.AddSingleton(mapper);

services.AddSingleton<SmtpClient>();
services.AddSingleton<IEmailClient, EmailClient>();
services.AddMailTemplate(config.GetConnectionString("IdentityConnection"), "Scene.Migrations.PostgreSql", config);

var work = new Work(HostName, QueueName);
work.Execute(DoAction, services);

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();

static async void DoAction(string message, IServiceCollection services)
{
    Console.WriteLine(message);
    ServiceProvider serviceProvider = services.BuildServiceProvider(); 
    var mailTemplateService = serviceProvider.GetService<IMailTemplateService>();

    // TODO в какой тип тут десериализовывать? Сделать свой для уменьшения связности?
    var data = JsonSerializer.Deserialize<MailDataModel>(message);

    var mail = await mailTemplateService.FormatMail(data.TemplatePermName, data.Culture, data.Variables.ToDictionary(v => v.Name, v => v.Value));

    var emailClient = serviceProvider.GetService<IEmailClient>();
    await emailClient.SendEmail(data.Email, mail.Title, mail.Body);
}

static IConfigurationRoot GetConfiguration()
{
    string appsettingsPath = Path.GetFullPath(
        Path.Combine(AppContext.BaseDirectory, "appsettings.json"));

    return new ConfigurationBuilder()
        .AddJsonFile(appsettingsPath, true, true)
        .Build();
}