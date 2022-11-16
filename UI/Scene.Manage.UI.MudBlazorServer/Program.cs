using Framework.Helpers.Http;
using Framework.MailTemplate;
using Framework.User.Service;
using Framework.User.Service.Contract;
using Framework.User.Service.Taskers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using MudBlazor.Services;
using Scene.Manage.UI.MudBlazorServer;
using Scene.Manage.UI.MudBlazorServer.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<RabbitMqConfiguration>(builder.Configuration.GetSection("RabbitMq"));

// Add services to the container.

builder.Services.AddMapper();

builder.Services.AddTransient<RegisterTasker>();

builder.Services.AddAppUser(builder.Configuration);


builder.Services.AddDataProtection()
                .PersistKeysToFileSystem(new System.IO.DirectoryInfo(@"D:\Data\Scene\Sessions"))
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

builder.Services.AddMailTemplate(builder.Configuration);

//AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

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