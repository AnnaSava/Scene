using Framework.Base.Types.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Base.DataService.Contract
{
    // TODO устарело. Заменить в контекстах, откуда брать конвеншен и префикс, и удалить
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
