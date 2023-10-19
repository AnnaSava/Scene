using SavaDev.Base.Unit.Options;
using SavaDev.Base.Unit;
using SavaDev.Content;
using SavaDev.Content.DemoApi;
using SavaDev.Content.Contract;
using SavaDev.Content.Services;
using SavaDev.Content.Client.Services;

var builder = WebApplication.CreateBuilder(args);

var userServer = builder.Configuration["UseServer"] == "true";

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

if(userServer)
{
    SetServerClientProfile(builder.Services, builder.Configuration);
}
else
{
    SetReferencedAssemblyProfile(builder.Services, builder.Configuration);
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

void SetServerClientProfile(IServiceCollection services, ConfigurationManager config)
{
    services.AddScoped<IDraftFrontService, DraftClientService>();
}

void SetReferencedAssemblyProfile(IServiceCollection services, ConfigurationManager config)
{
    services.AddMapper();
    services.AddContent(config, new UnitOptions("Cnt", AppSettings.DefaultConnectionStringPattern));
    services.AddContentFront();
}