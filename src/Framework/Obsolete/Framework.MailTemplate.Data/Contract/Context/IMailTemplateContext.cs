using Framework.Base.DataService.Contract.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.MailTemplate.Data.Contract.Context
{
    public interface IMailTemplateContext : IDbContext
    {
        DbSet<Data.Entities.MailTemplate> MailTemplates { get; set; }
    }
}
