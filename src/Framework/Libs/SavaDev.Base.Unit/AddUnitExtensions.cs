using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SavaDev.Base.Data.Context;
using SavaDev.Base.Data.Enums;
using SavaDev.Base.Unit.Options;
using SavaDev.Infrastructure;

namespace SavaDev.Base.Unit
{
    public static class AddUnitExtensions
    {
        public static void AddUnitDbContext<TContext> (this IServiceCollection services, IConfiguration config, UnitOptions unitOptions)
            where TContext : DbContext
        {
            var parameters = new Parameters(config, unitOptions);
            services.AddDbContext<TContext>(options => SetOptions(options, parameters));
        }

        public static void AddUnitDbContext<TInterface, TContext> (this IServiceCollection services, IConfiguration config, UnitOptions unitOptions)
            where TContext : DbContext, TInterface
        {
            var parameters = new Parameters(config, unitOptions);
            services.AddDbContext<TInterface, TContext>(options => SetOptions(options, parameters));
        }

        public static void SetSharedCookie(this IServiceCollection services, IConfiguration config, string appName)
        {
            var sharedCookie = config.GetSection("SharedCookie").Get<SharedCookieConfiguration>();
            services.AddScoped(s => sharedCookie);

            services.AddDataProtection()
                .PersistKeysToFileSystem(new DirectoryInfo(config.GetSection("SessionsPath").Value))
                .SetApplicationName(appName);

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = sharedCookie.Name;
                if (!string.IsNullOrEmpty(sharedCookie.SameSite))
                {
                    Enum.TryParse(sharedCookie.SameSite, out SameSiteMode sameSiteMode);
                    options.Cookie.SameSite = sameSiteMode;
                }
                if (!string.IsNullOrEmpty(sharedCookie.Domain))
                {
                    options.Cookie.Domain = sharedCookie.Domain;
                }
            });
        }

        public static void SetAcceptSharedCookie(this IServiceCollection services, IConfiguration config, string appName)
        {
            services.AddDataProtection()
                .PersistKeysToFileSystem(new DirectoryInfo(config.GetSection("SessionsPath").Value))
                .SetApplicationName(appName);

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = config["Cookie:Name"];
                options.Events = new CookieAuthenticationEvents()
                {
                    OnRedirectToLogin = (context) =>
                    {
                        var uri = UriHelpers.MakeLoginRedirectUri(context.Request, config["Cookie:LoginUrl"]);
                        context.HttpContext.Response.Redirect(uri);
                        return Task.CompletedTask;
                    }
                };
            });
        }

        private static DbContextOptionsBuilder SetOptions(DbContextOptionsBuilder options, Parameters p)
        {
            if (p.DbProvider == DbProviderName.DefaultPgName)
            {
                options.UseNpgsql(p.Connection, b => b.MigrationsAssembly(p.Migrations));
            }
            else if (p.DbProvider == DbProviderName.DefaultMsName)
            {
                options.UseSqlServer(p.Connection, b => b.MigrationsAssembly(p.Migrations));
            }

            var tablePrefix = p.TablePrefix;
            if (p.NamingConvention == NamingConvention.SnakeCase)
            {
                options.UseSnakeCaseNamingConvention();
                tablePrefix = tablePrefix.ToLower() + "_";
            }
            else
            {
                tablePrefix = tablePrefix + ".";
            }

            var extension = options.Options.FindExtension<BaseDbOptionsExtension>()
                ?? new BaseDbOptionsExtension { TablePrefix = tablePrefix, NamingConvention = p.NamingConvention };

            ((IDbContextOptionsBuilderInfrastructure)options).AddOrUpdateExtension(extension);
            return options;
        }

        class Parameters
        {
            public string DbProvider { get; set; }

            public string Connection { get; set; }

            public string Migrations { get; set; }

            public string TablePrefix { get; }

            public NamingConvention NamingConvention { get; set; }

            public Parameters(IConfiguration config, UnitOptions serviceOptions)
            {
                DbProvider = config.GetDbProvider();
                Connection = config.GetConnectionString(string.Format(serviceOptions.ConnectionStringName, DbProvider));
                Migrations = config.GetMigrationAssemblyString(DbProvider);
                TablePrefix = serviceOptions.TablePrefix;
                NamingConvention = config.GetSqlNamingConvention(DbProvider);
            }
        }
    }
}