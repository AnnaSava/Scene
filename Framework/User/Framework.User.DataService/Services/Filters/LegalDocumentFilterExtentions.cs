using Framework.Base.DataService.Services;
using Framework.Base.Types.Enums;
using Framework.User.DataService.Contract.Models;
using Framework.User.DataService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.DataService.Services
{
    public static class LegalDocumentFilterExtentions
    {
        public static IQueryable<LegalDocument> ApplyFilters(this IQueryable<LegalDocument> list, LegalDocumentFilterModel filter)
        {
            if (filter == null) return list;

            return list
                .ApplyIsDeletedFilter(filter)
                .ApplyIdFilter(filter)
                .ApplyIsApprovedFilter(filter)
                .ApplyPermNameFilter(filter);
        }

        private static IQueryable<LegalDocument> ApplyIsApprovedFilter(this IQueryable<LegalDocument> list, LegalDocumentFilterModel filter)
        {
            if (filter.IsApproved != null)
            {
                list = list.Where(m => m.IsApproved == filter.IsApproved);
            }
            return list;
        }

        private static IQueryable<LegalDocument> ApplyPermNameFilter(this IQueryable<LegalDocument> list, LegalDocumentFilterModel filter)
        {
            if (filter.PermName?.Value?.Any() ?? false)
            {
                var rules = GetPermNameRules(filter.PermName.Value);
                list = list.Where(rules[filter.PermName.MatchMode]);
            }
            return list;
        }

        private static IQueryable<LegalDocument> ApplyCultureFilter(this IQueryable<LegalDocument> list, LegalDocumentFilterModel filter)
        {
            if (filter.Culture?.Value?.Any() ?? false)
            {
                var rules = GetCultureRules(filter.Culture.Value);
                list = list.Where(rules[filter.Culture.MatchMode]);
            }
            return list;
        }

        private static IQueryable<LegalDocument> ApplyTitleFilter(this IQueryable<LegalDocument> list, LegalDocumentFilterModel filter)
        {
            if (filter.Title?.Value?.Any() ?? false)
            {
                var rules = GetTitleRules(filter.Title.Value);
                list = list.Where(rules[filter.Title.MatchMode]);
            }
            return list;
        }

        private static Dictionary<MatchModeWord, Expression<Func<LegalDocument, bool>>> GetPermNameRules(List<string> value)
        {
            return new Dictionary<MatchModeWord, Expression<Func<LegalDocument, bool>>>()
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

        private static Dictionary<MatchModeWord, Expression<Func<LegalDocument, bool>>> GetCultureRules(List<string> value)
        {
            return new Dictionary<MatchModeWord, Expression<Func<LegalDocument, bool>>>()
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

        private static Dictionary<MatchModeWord, Expression<Func<LegalDocument, bool>>> GetTitleRules(List<string> value)
        {
            return new Dictionary<MatchModeWord, Expression<Func<LegalDocument, bool>>>()
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
