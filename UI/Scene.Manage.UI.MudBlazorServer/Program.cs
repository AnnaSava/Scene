using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Scene.Manage.UI.MudBlazorServer.Data;
using MudBlazor.Services;
using Scene.Manage.UI.MudBlazorServer;
using Framework.User.DataService.Services;
using Microsoft.EntityFrameworkCore;
using Framework.User.Service;
using Framework.MailTemplate;
using Framework.User.Service.Contract;
using Framework.User.Service.Taskers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<RabbitMqConfiguration>(builder.Configuration.GetSection("RabbitMq"));

// Add services to the container.

builder.Services.AddMapper();

builder.Services.AddDbContext<FrameworkUserDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("IdentityConnection"), b => b.MigrationsAssembly("Scene.Migrations.PostgreSql")));

builder.Services.AddTransient<RegisterTasker>();

builder.Services.AddMailTemplate(builder.Configuration.GetConnectionString("IdentityConnection"), "Scene.Migrations.PostgreSql", builder.Configuration);

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddFrameworkUser(builder.Configuration);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddMudServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();