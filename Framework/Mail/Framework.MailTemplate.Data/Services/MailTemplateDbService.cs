using AutoMapper;
using Framework.Base.DataService.Contract.Interfaces;
using Framework.Base.DataService.Contract.Models;
using Framework.Base.DataService.Contract.Models.ListView;
using Framework.Base.DataService.Services;
using Framework.MailTemplate.Data.Contract;
using Framework.MailTemplate.Data.Contract.Context;
using Framework.MailTemplate.Data.Contract.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.MailTemplate.Data.Services
{
    public class MailTemplateDbService : BaseEntityService<Entities.MailTemplate, MailTemplateModel>, IMailTemplateDbService
    {
        public MailTemplateDbService(IMailTemplateContext dbContext, IMapper mapper)
            : base(dbContext, mapper, nameof(MailTemplateDbService))
        {

        }

        public async Task<PageListModel<MailTemplateModel>> GetAll(ListQueryModel<MailTemplateFilterModel> query)
        {
            return await _dbContext.GetAll<Entities.MailTemplate, MailTemplateModel, MailTemplateFilterModel>(query, ApplyFilters, _mapper);
        }

        public async Task<bool> CheckDocumentExisis(string permName)
        {
            return await _dbContext.Set<Entities.MailTemplate>().AnyAsync(m => m.PermName == permName);
        }

        public async Task<bool> CheckTranslationExisis(string permName, string culture)
        {
            return await _dbContext.Set<Entities.MailTemplate>().AnyAsync(m => m.PermName == permName && m.Culture == culture);
        }

        protected void ApplyFilters(ref IQueryable<Entities.MailTemplate> list, MailTemplateFilterModel filter)
        {
            list = list.ApplyFilters(filter);
        }
    }
}
