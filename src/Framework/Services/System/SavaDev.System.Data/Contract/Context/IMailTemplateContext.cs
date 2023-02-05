using Microsoft.EntityFrameworkCore;
using SavaDev.Base.Data.Context;
using SavaDev.System.Data.Entities;

namespace SavaDev.System.Data.Contract.Context
{
    public interface IMailTemplateContext : IDbContext
    {
        DbSet<MailTemplate> MailTemplates { get; set; }
    }
}
