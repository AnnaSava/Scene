using Framework.Base.DataService.Services;
using Framework.Base.Types.Enums;
using Framework.MailTemplate.Data.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Framework.MailTemplate.Data.Services
{
    public static class MailTemplateFilterExtentions
    {
        public static IQueryable<Entities.MailTemplate> ApplyFilters(this IQueryable<Entities.MailTemplate> list, MailTemplateFilterModel filter)
        {
            if (filter == null) return list;

            return list
                .ApplyIsDeletedFilter(filter)
                .ApplyIdFilter(filter)
                .ApplyStatusFilter(filter)
                .ApplyPermNameFilter(filter)
                .ApplyCultureFilter(filter)
                .ApplyTitleFilter(filter);
        }

        private static IQueryable<Entities.MailTemplate> ApplyStatusFilter(this IQueryable<Entities.MailTemplate> list, MailTemplateFilterModel filter)
        {
            if (filter.Status != null)
            {
                list = list.Where(m => m.Status == filter.Status);
            }
            return list;
        }

        private static IQueryable<Entities.MailTemplate> ApplyPermNameFilter(this IQueryable<Entities.MailTemplate> list, MailTemplateFilterModel filter)
        {
            if (filter.PermName?.Value?.Any() ?? false)
            {
                var rules = GetPermNameRules(filter.PermName.Value);
                list = list.Where(rules[filter.PermName.MatchMode]);
            }
            return list;
        }

        private static IQueryable<Entities.MailTemplate> ApplyCultureFilter(this IQueryable<Entities.MailTemplate> list, MailTemplateFilterModel filter)
        {
            if (filter.Culture?.Value?.Any() ?? false)
            {
                var rules = GetCultureRules(filter.Culture.Value);
                list = list.Where(rules[filter.Culture.MatchMode]);
            }
            return list;
        }

        private static IQueryable<Entities.MailTemplate> ApplyTitleFilter(this IQueryable<Entities.MailTemplate> list, MailTemplateFilterModel filter)
        {
            if (filter.Title?.Value?.Any() ?? false)
            {
                var rules = GetTitleRules(filter.Title.Value);
                list = list.Where(rules[filter.Title.MatchMode]);
            }
            return list;
        }

        private static Dictionary<MatchModeWord, Expression<Func<Entities.MailTemplate, bool>>> GetPermNameRules(List<string> value)
        {
            return new Dictionary<MatchModeWord, Expression<Func<Entities.MailTemplate, bool>>>()
                    {
                        { MatchModeWord.Equals, m => m.PermName == value.First() },
                        { MatchModeWord.NotEquals, m => m.PermName != value.First() },
                        { MatchModeWord.StartsWith, m => m.PermName.StartsWith(value.First()) },
                        { MatchModeWord.EndsWith, m => m.PermName.EndsWith(value.First()) },
                        { MatchModeWord.Contains, m => m.PermName.Contains(value.First()) },
                        { MatchModeWord.NotContains, m => !m.PermName.Contains(value.First()) },
                        { MatchModeWord.In, m => value.Contains(m.PermName) },
                        { MatchModeWord.NotIn, m => !value.Contains(m.PermName) },
                    };
        }

        private static Dictionary<MatchModeWord, Expression<Func<Entities.MailTemplate, bool>>> GetCultureRules(List<string> value)
        {
            return new Dictionary<MatchModeWord, Expression<Func<Entities.MailTemplate, bool>>>()
                    {
                        { MatchModeWord.Equals, m => m.Culture == value.First() },
                        { MatchModeWord.NotEquals, m => m.Culture != value.First() },
                        { MatchModeWord.StartsWith, m => m.Culture.StartsWith(value.First()) },
                        { MatchModeWord.EndsWith, m => m.Culture.EndsWith(value.First()) },
                        { MatchModeWord.Contains, m => m.Culture.Contains(value.First()) },
                        { MatchModeWord.NotContains, m => !m.Culture.Contains(value.First()) },
                        { MatchModeWord.In, m => value.Contains(m.Culture) },
                        { MatchModeWord.NotIn, m => !value.Contains(m.Culture) },
                    };
        }

        private static Dictionary<MatchModeWord, Expression<Func<Entities.MailTemplate, bool>>> GetTitleRules(List<string> value)
        {
            return new Dictionary<MatchModeWord, Expression<Func<Entities.MailTemplate, bool>>>()
                    {
                        { MatchModeWord.Equals, m => m.Title == value.First() },
                        { MatchModeWord.NotEquals, m => m.Title != value.First() },
                        { MatchModeWord.StartsWith, m => m.Title.StartsWith(value.First()) },
                        { MatchModeWord.EndsWith, m => m.Title.EndsWith(value.First()) },
                        { MatchModeWord.Contains, m => m.Title.Contains(value.First()) },
                        { MatchModeWord.NotContains, m => !m.Title.Contains(value.First()) },
                        { MatchModeWord.In, m => value.Contains(m.Title) },
                        { MatchModeWord.NotIn, m => !value.Contains(m.Title) },
                    };
        }
    }
}
