﻿using Framework.Base.Types;
using Framework.MailTemplate.Data.Contract.Context;
using Framework.MailTemplate.SeedLib.Data;

namespace Framework.MailTemplate.SeedLib
{
    public class MailTemplateSeeder : ISeeder
    {
        private readonly IMailTemplateContext context;
        public MailTemplateSeeder(IMailTemplateContext dbContext)
        {
            context = dbContext;
        }

        public async Task Seed()
        {
            if (context.MailTemplates.Any()) return;

            var en = MailTemplateData.MailTemplateEnCulture
                .Select(m => new MailTemplate.Data.Entities.MailTemplate
                {
                    PermName = m.Key,
                    Title = m.Value,
                    Text = "This is a new template. Edit this text as you wish.",
                    Created = DateTime.Now,
                    LastUpdated = DateTime.Now,
                    Status = Base.Types.Enums.DocumentStatus.Draft,
                    Culture = "en"
                });

            var ru = MailTemplateData.MailTemplateRuCulture
                .Select(m => new MailTemplate.Data.Entities.MailTemplate
                {
                    PermName = m.Key,
                    Title = m.Value,
                    Text = "Это новый шаблон. Отредактиуйте его так, как вам нужно.",
                    Created = DateTime.Now,
                    LastUpdated = DateTime.Now,
                    Status = Base.Types.Enums.DocumentStatus.Draft,
                    Culture = "ru"
                });

            context.MailTemplates.AddRange(en);
            context.MailTemplates.AddRange(ru);

            await context.SaveChangesAsync();
        }
    }
}