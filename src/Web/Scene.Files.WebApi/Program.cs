using EasyNetQ;
using Sava.Files;
using SavaDev.Base.Unit;
using SavaDev.Base.Unit.Options;
using SavaDev.Files.Service;
using SavaDev.Files.Service.Contract;
using SavaDev.Files.Service.Contract.Models;
using Scene.Files.WebApi;
using Scene.Libs.WebModule;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMapper();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddFiles(builder.Configuration, new UnitOptions(SceneUnitCode.Files, AppSettings.DefaultConnectionStringPattern));
builder.Services.AddFilesRmqService(builder.Configuration);

using (var bus = RabbitHutch.CreateBus("host=localhost"))
{
    await bus.Rpc.RespondAsync<FilesDataModel, FilesUploadResult>(HandleMessage);
    Console.WriteLine("Listening for messages. Hit <return> to quit.");
    Console.ReadLine();
}

async Task<FilesUploadResult> HandleMessage(FilesDataModel data)
{
    ServiceProvider serviceProvider = builder.Services.BuildServiceProvider();
    try
    {
        var fileProcessingService = serviceProvider.GetService<IFileProcessingService>();

        var resultModel = await fileProcessingService.UploadFilePreventDuplicate(data.Content);

        return new FilesUploadResult { SavedFile = resultModel };
    }
    catch (Exception ex)
    {
        var t = ex.Message;
        return new FilesUploadResult();
    }
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