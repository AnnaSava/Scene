using Framework.Base.Types.Enums;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Helpers.Config
{
    public static class ConfigExtentions
    {

        // TODO разобраться, что это
        public static string GetMigrationAssemblyString(this IConfiguration config)
        {
            return config["MigrationsAssembly"];
        }

        public static NamingConvention GetSqlNamingConvention(this IConfiguration config)
        {
            var conv = config["SqlNamingConvention"];
            var parsed = Enum.TryParse(typeof(NamingConvention), conv, out object result);
            return parsed ? (NamingConvention)result : NamingConvention.Default;
        }
    }
}
