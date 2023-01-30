﻿using Microsoft.Extensions.Configuration;
using SavaDev.Base.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Unit
{
    public static class ConfigExtentions
    {
        // TODO разобраться, что это
        public static string GetMigrationAssemblyString(this IConfiguration config)
        {
            return config.GetSection("MigrationsAssemblies:Default").Value;
        }

        public static NamingConvention GetSqlNamingConvention(this IConfiguration config)
        {
            var conv = config["SqlNamingConvention"];
            var parsed = Enum.TryParse(typeof(NamingConvention), conv, out object result);
            return parsed ? (NamingConvention)result : NamingConvention.Default;
        }
    }
}