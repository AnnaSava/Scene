using EasyNetQ;
using Sava.Libs.WebModule;
using SavaDev.Base.Unit;
using SavaDev.Base.Unit.Options;
using SavaDev.Mail.Service;
using SavaDev.Mail.Service.Contract;
using SavaDev.Mail.Service.Contract.Models;
using SavaDev.System.Front;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSystem(builder.Configuration, new UnitOptions(UnitCode.System, AppSettings.DefaultConnectionStringPattern));
builder.Services.AddMailRmqService(builder.Configuration);

using (var bus = RabbitHutch.CreateBus("host=localhost"))
{
    await bus.Rpc.RespondAsync<MailDataModel, MailSendResult>(HandleMessage);
    Console.WriteLine("Listening for messages. Hit <return> to quit.");
    Console.ReadLine();
}

async Task<MailSendResult> HandleMessage(MailDataModel data)
{
    ServiceProvider serviceProvider = builder.Services.BuildServiceProvider();
    var mailService = serviceProvider.GetService<IMailService>();

    var resultModel = await mailService.FormatAndSendEmail(data);

    return resultModel;
}


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
