using SavaDev.Base.Data.Enums;
using SavaDev.System.Data.Entities;
using SavaDev.System.Data.Seeder.Data;

namespace SavaDev.System.Data.Seeder
{
    public class LegalDocumentSeeder
    {
        private readonly SystemContext context;

        public LegalDocumentSeeder(SystemContext dbContext)
        {
            context = dbContext;
        }

        public async Task Seed()
        {
            if (context.LegalDocuments.Any()) return;

            var en = LegalDocumentData.LegalDocumentEnCulture
                .Select(m => new LegalDocument
                {
                    PermName = m.Key,
                    Title = m.Value,
                    Text = "This is a new document. Edit this text as you wish.",
                    Created = DateTime.UtcNow,
                    LastUpdated = DateTime.UtcNow,
                    Status = DocumentStatus.Draft,
                    Culture = "en"
                });

            var ru = LegalDocumentData.LegalDocumentRuCulture
                .Select(m => new LegalDocument
                {
                    PermName = m.Key,
                    Title = m.Value,
                    Text = "Это новый документ. Отредактиуйте его так, как вам нужно.",
                    Created = DateTime.UtcNow,
                    LastUpdated = DateTime.UtcNow,
                    Status = DocumentStatus.Draft,
                    Culture = "ru"
                });

            context.LegalDocuments.AddRange(en);
            context.LegalDocuments.AddRange(ru);

            await context.SaveChangesAsync();
        }
    }
}
