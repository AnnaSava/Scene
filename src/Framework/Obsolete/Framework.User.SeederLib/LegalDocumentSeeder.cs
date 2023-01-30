using Framework.Base.Types.Enums;
using Framework.User.DataService.Contract.Interfaces.Context;
using Framework.User.DataService.Entities;
using Framework.User.SeederLib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.SeederLib
{
    public class LegalDocumentSeeder
    {
        private readonly ILegalDocumentContext context;

        public LegalDocumentSeeder(ILegalDocumentContext dbContext)
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
                    Created = DateTime.Now,
                    LastUpdated = DateTime.Now,
                    Status = DocumentStatus.Draft,
                    Culture = "en"
                });

            var ru = LegalDocumentData.LegalDocumentRuCulture
                .Select(m => new LegalDocument
                {
                    PermName = m.Key,
                    Title = m.Value,
                    Text = "Это новый документ. Отредактиуйте его так, как вам нужно.",
                    Created = DateTime.Now,
                    LastUpdated = DateTime.Now,
                    Status = DocumentStatus.Draft,
                    Culture = "ru"
                });

            context.LegalDocuments.AddRange(en);
            context.LegalDocuments.AddRange(ru);

            await context.SaveChangesAsync();
        }
    }
}
