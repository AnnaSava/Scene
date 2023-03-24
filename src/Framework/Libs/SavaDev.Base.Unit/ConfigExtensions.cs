using Microsoft.Extensions.Configuration;
using SavaDev.Base.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Unit
{
    public static class ConfigExtensions
    {
        public static string GetDbProvider(this IConfiguration config)
        {
            return config.GetSection("DbProvider").Value;
        }

        // TODO разобраться, что это
        public static string GetMigrationAssemblyString(this IConfiguration config, string DbProvider)
        {
            return config.GetSection($"MigrationsAssemblies:Default{DbProvider}").Value;
        }

        public static NamingConvention GetSqlNamingConvention(this IConfiguration config, string DbProvider)
        {
            var conv = config[$"{DbProvider}SqlNamingConvention"];
            var parsed = Enum.TryParse(typeof(NamingConvention), conv, out object result);
            return parsed ? (NamingConvention)result : NamingConvention.Default;
        }
    }
}
