using Microsoft.EntityFrameworkCore;
using SavaDev.Base.Data.Context;
using SavaDev.General.Data.Entities;

namespace SavaDev.General.Data.Contract.Context
{
    public interface IMailTemplateContext : IDbContext
    {
        DbSet<MailTemplate> MailTemplates { get; set; }
    }
}
