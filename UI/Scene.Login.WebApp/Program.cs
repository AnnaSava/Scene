using Framework.MailTemplate;
using Framework.User.DataService.Services;
using Framework.User.Service;
using Framework.User.Service.Contract;
using Framework.User.Service.Taskers;
using Microsoft.EntityFrameworkCore;
using Scene.Login.WebApp;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<RabbitMqConfiguration>(builder.Configuration.GetSection("RabbitMq"));

// Add services to the container.
builder.Services.AddMapper();

builder.Services.AddDbContext<AppUserContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("IdentityConnection"), b => b.MigrationsAssembly("Scene.Migrations.PostgreSql")));

builder.Services.AddAppUser(builder.Configuration);
builder.Services.AddTransient<RegisterTasker>();

builder.Services.AddMailTemplate(builder.Configuration);

//AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddRazorPages();

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

app.MapRazorPages();

app.Run();
