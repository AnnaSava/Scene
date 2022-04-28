using Framework.Base.Types.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Base.DataService.Contract
{
    public class DbContextSettings<TContext>
        where TContext: DbContext
    {
        public string TablePrefix { get; }

        public NamingConvention NamingConvention { get; set; }

        public DbContextSettings(string tablePrefix, NamingConvention namingConvention)
        {
            TablePrefix = tablePrefix;
            NamingConvention = namingConvention;
        }
    }
}
