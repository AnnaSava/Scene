using SavaDev.Base.Data.Seeder;
using SavaDev.General.Data.Entities;
using SavaDev.General.Data.Seeder.Data;

namespace SavaDev.General.Data.Seeder
{
    public class ReservedNameSeeder : ISeeder
    {
        private readonly GeneralContext context;

        public ReservedNameSeeder(GeneralContext dbContext)
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
