using SavaDev.Base.Data.Context;
using SavaDev.System.Data.Entities;
using SavaDev.System.Data.Seeder.Data;

namespace SavaDev.System.Data.Seeder
{
    public class ReservedNameSeeder : ISeeder
    {
        private readonly SystemContext context;

        public ReservedNameSeeder(SystemContext dbContext)
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
