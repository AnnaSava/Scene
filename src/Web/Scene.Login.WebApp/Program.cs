using Microsoft.AspNetCore.DataProtection;
using SavaDev.Base.Unit;
using SavaDev.Base.Unit.Options;
using SavaDev.Infrastructure;
using SavaDev.Mail.Service;
using SavaDev.General.Front;
using SavaDev.Users.Front;
using Scene.Libs.WebModule;
using Scene.Login.WebApp;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<RabbitMqConfiguration>(builder.Configuration.GetSection("RabbitMq"));

// Add services to the container.
builder.Services.AddMapper();

builder.Services.AddGeneral(builder.Configuration, new UnitOptions(SceneUnitCode.General, AppSettings.DefaultConnectionStringPattern));
builder.Services.AddUsers(builder.Configuration, new UnitOptions(SceneUnitCode.AppUsers, AppSettings.DefaultConnectionStringPattern));
builder.Services.AddMailRmqService(builder.Configuration);

builder.Services.AddDataProtection()
                .PersistKeysToFileSystem(new System.IO.DirectoryInfo(builder.Configuration.GetSection("SessionsPath").Value))
                .SetApplicationName("SceneApp");

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = ".Scene.SharedCookie";
});

builder.Services.AddRazorPages();

try
{
    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    //app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapRazorPages();

    app.Run();
}
catch (Exception ex)
{
    var t = ex.Message;
}
