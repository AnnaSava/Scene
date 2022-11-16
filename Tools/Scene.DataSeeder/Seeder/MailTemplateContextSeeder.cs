using Framework.Base.Types;
using Framework.MailTemplate.Data;
using Framework.MailTemplate.SeedLib;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scene.DataSeeder
{
    internal class MailTemplateContextSeeder : ISeeder
    {
        private readonly MailTemplateContext context;

        public MailTemplateContextSeeder(MailTemplateContext dbContext)
        {
            context = dbContext;
        }

        public async Task Seed()
        {
            context.Database.Migrate();

            await new MailTemplateSeeder(context).Seed();
        }
    }
}
