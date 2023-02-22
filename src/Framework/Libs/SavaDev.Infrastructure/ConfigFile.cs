using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Infrastructure
{
    public static class ConfigFile
    {
        public static string GetEnvironment()
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            return string.IsNullOrEmpty(env) ? "Production" : env;
        }

        public static IConfigurationRoot GetConfiguration(string environmentName)
        {
            string appsettingsPath = Path.GetFullPath(
                Path.Combine(AppContext.BaseDirectory, "appsettings.json"));

            string appsettingsEnvPath = Path.GetFullPath(
                Path.Combine(AppContext.BaseDirectory, $"appsettings.{environmentName}.json"));

            return new ConfigurationBuilder()
                .AddJsonFile(appsettingsPath, true, true)
                .AddJsonFile(appsettingsEnvPath, true, true)
                .Build();
        }
    }
}
