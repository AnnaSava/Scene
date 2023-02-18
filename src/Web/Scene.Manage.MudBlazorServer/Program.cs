using Framework.Helpers.Http;
using Framework.User.Service.Contract;
using Framework.User.Service.Taskers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using MudBlazor.Services;
using Sava.Libs.WebModule;
using SavaDev.Base.Unit.Options;
using SavaDev.Base.Unit;
using SavaDev.System.Front;
using SavaDev.Users.Front;
using Scene.Manage.UI.MudBlazorServer;
using Scene.Manage.UI.MudBlazorServer.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<RabbitMqConfiguration>(builder.Configuration.GetSection("RabbitMq"));

// Add services to the container.

//var opt = config.GetSection("ServiceOptions").Get<ServiceOptions>();
//services.AddScoped(s => opt);

//var copt = config.GetSection("Cultures").Get<AvailableContentCultures>();
//services.AddScoped(s => copt);

//services.AddHttpContextAccessor();
//services.AddCurrentUser();

builder.Services.AddMapper();

builder.Services.AddTransient<RegisterTasker>();

builder.Services.AddUsers(builder.Configuration, new UnitOptions(SavaUnitCode.AppUsers, AppSettings.DefaultConnectionStringPattern));
builder.Services.AddSystem(builder.Configuration, new UnitOptions(SavaUnitCode.System, AppSettings.DefaultConnectionStringPattern));

builder.Services.AddDataProtection()
                .PersistKeysToFileSystem(new DirectoryInfo(builder.Configuration.GetSection("SessionsPath").Value))
                .SetApplicationName("SceneApp");

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = builder.Configuration["Cookie:Name"];
    options.Events = new CookieAuthenticationEvents()
    {
        OnRedirectToLogin = (context) =>
        {
            var uri = UriHelpers.MakeLoginRedirectUri(context.Request, builder.Configuration["Cookie:LoginUrl"]);
            context.HttpContext.Response.Redirect(uri);
            return Task.CompletedTask;
        }
    };
});

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

app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();