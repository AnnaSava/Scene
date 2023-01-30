using Framework.Base.Types;
using Framework.User.DataService.Contract.Interfaces;
using Framework.User.DataService.Entities;
using Framework.User.SeederLib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.SeederLib
{
    public class ReservedNameSeeder : ISeeder
    {
        private readonly IReservedNameContext context;

        public ReservedNameSeeder(IReservedNameContext dbContext)
        {
            context = dbContext;
        }

        public async Task Seed()
        {
            if (context.ReservedNames.Any()) return;

            var withS = ReservedNameInitialData.WithPlurals.Select(m => new ReservedName { Text = m, IncludePlural = true });
            var withoutS = ReservedNameInitialData.WithoutPlurals.Select(m => new ReservedName { Text = m, IncludePlural = false });

            context.ReservedNames.AddRange(withS);
            context.ReservedNames.AddRange(withoutS);

            await context.SaveChangesAsync();
        }
    }
}
