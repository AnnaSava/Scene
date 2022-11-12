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
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<RabbitMqConfiguration>(builder.Configuration.GetSection("RabbitMq"));

// Add services to the container.

builder.Services.AddMapper();

// Из видео https://www.youtube.com/watch?v=iq2btD9WufI
builder.Services.AddAuthenticationCore();

builder.Services.AddDbContext<FrameworkUserDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("IdentityConnection"), b => b.MigrationsAssembly("Scene.Migrations.PostgreSql")));

builder.Services.AddTransient<RegisterTasker>();

builder.Services.AddMailTemplate(builder.Configuration);

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddFrameworkUser(builder.Configuration);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<ProtectedSessionStorage>();
builder.Services.AddScoped<AuthenticationStateProvider, SceneAuthenticationStateProvider>();
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

app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();