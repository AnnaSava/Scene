using SavaDev.Base.Data.Context;
using SavaDev.Base.Data.Enums;
using SavaDev.General.Data.Entities;
using SavaDev.General.Data.Seeder.Data;

namespace SavaDev.General.Data.Seeder
{
    public class MailTemplateSeeder : ISeeder
    {
        private readonly GeneralContext context;

        public MailTemplateSeeder(GeneralContext dbContext)
        {
            context = dbContext;
        }

        public async Task Seed()
        {
            if (context.MailTemplates.Any()) return;

            var en = MailTemplateData.MailTemplateEnCulture
                .Select(m => new MailTemplate
                {
                    PermName = m.Key,
                    Title = m.Value,
                    Text = "This is a new template. Edit this text as you wish.",
                    Created = DateTime.UtcNow,
                    LastUpdated = DateTime.UtcNow,
                    Status = DocumentStatus.Draft,
                    Culture = "en"
                });

            var ru = MailTemplateData.MailTemplateRuCulture
                .Select(m => new MailTemplate
                {
                    PermName = m.Key,
                    Title = m.Value,
                    Text = "Это новый шаблон. Отредактиуйте его так, как вам нужно.",
                    Created = DateTime.UtcNow,
                    LastUpdated = DateTime.UtcNow,
                    Status = DocumentStatus.Draft,
                    Culture = "ru"
                });

            context.MailTemplates.AddRange(en);
            context.MailTemplates.AddRange(ru);

            await context.SaveChangesAsync();
        }
    }
}
