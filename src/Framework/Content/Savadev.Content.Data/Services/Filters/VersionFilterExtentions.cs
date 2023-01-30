using Framework.Base.Types.Enums;
using Savadev.Content.Data.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Savadev.Content.Data.Services.Filters
{
    public static class VersionFilterExtentions
    {
        public static IQueryable<Entities.Version> ApplyFilters(this IQueryable<Entities.Version> list, VersionFilterModel filter)
        {
            if (filter == null) return list;

            return list.ApplyModuleFilter(filter)
                .ApplyEntityFilter(filter)
                .ApplyOwnerFilter(filter)
                .ApplyContentIdFilter(filter);
        }

        private static IQueryable<Entities.Version> ApplyModuleFilter(this IQueryable<Entities.Version> list, VersionFilterModel filter)
        {
            if (filter.Module?.Value?.Any() ?? false)
            {
                var rules = GetModuleRules(filter.Module.Value);
                list = list.Where(rules[filter.Module.MatchMode]);
            }
            return list;
        }

        private static IQueryable<Entities.Version> ApplyEntityFilter(this IQueryable<Entities.Version> list, VersionFilterModel filter)
        {
            if (filter.Entity?.Value?.Any() ?? false)
            {
                var rules = GetEntityRules(filter.Entity.Value);
                list = list.Where(rules[filter.Entity.MatchMode]);
            }
            return list;
        }

        private static IQueryable<Entities.Version> ApplyContentIdFilter(this IQueryable<Entities.Version> list, VersionFilterModel filter)
        {
            if (filter.ContentId?.Value?.Any() ?? false)
            {
                var rules = GetContentIdRules(filter.ContentId.Value);
                list = list.Where(rules[filter.ContentId.MatchMode]);
            }
            return list;
        }

        private static IQueryable<Entities.Version> ApplyOwnerFilter(this IQueryable<Entities.Version> list, VersionFilterModel filter)
        {
            if (filter.Owner?.Value?.Any() ?? false)
            {
                var rules = GetOwnerRules(filter.Owner.Value);
                list = list.Where(rules[filter.Owner.MatchMode]);
            }
            return list;
        }

        private static Dictionary<MatchModeWord, Expression<Func<Entities.Version, bool>>> GetModuleRules(List<string> value)
        {
            return new Dictionary<MatchModeWord, Expression<Func<Entities.Version, bool>>>()
                    {
                        { MatchModeWord.Equals, m => m.Module == value.First() },
                        { MatchModeWord.NotEquals, m => m.Module != value.First() },
                        { MatchModeWord.StartsWith, m => m.Module.StartsWith(value.First()) },
                        { MatchModeWord.EndsWith, m => m.Module.EndsWith(value.First()) },
                        { MatchModeWord.Contains, m => m.Module.Contains(value.First()) },
                        { MatchModeWord.NotContains, m => !m.Module.Contains(value.First()) },
                        { MatchModeWord.In, m => value.Contains(m.Module) },
                        { MatchModeWord.NotIn, m => !value.Contains(m.Module) },
                    };
        }

        private static Dictionary<MatchModeWord, Expression<Func<Entities.Version, bool>>> GetEntityRules(List<string> value)
        {
            return new Dictionary<MatchModeWord, Expression<Func<Entities.Version, bool>>>()
                    {
                        { MatchModeWord.Equals, m => m.Entity == value.First() },
                        { MatchModeWord.NotEquals, m => m.Entity != value.First() },
                        { MatchModeWord.StartsWith, m => m.Entity.StartsWith(value.First()) },
                        { MatchModeWord.EndsWith, m => m.Entity.EndsWith(value.First()) },
                        { MatchModeWord.Contains, m => m.Entity.Contains(value.First()) },
                        { MatchModeWord.NotContains, m => !m.Entity.Contains(value.First()) },
                        { MatchModeWord.In, m => value.Contains(m.Entity) },
                        { MatchModeWord.NotIn, m => !value.Contains(m.Entity) },
                    };
        }

        private static Dictionary<MatchModeWord, Expression<Func<Entities.Version, bool>>> GetContentIdRules(List<string> value)
        {
            return new Dictionary<MatchModeWord, Expression<Func<Entities.Version, bool>>>()
                    {
                        { MatchModeWord.Equals, m => m.ContentId == value.First() },
                        { MatchModeWord.NotEquals, m => m.ContentId != value.First() },
                        { MatchModeWord.StartsWith, m => m.ContentId.StartsWith(value.First()) },
                        { MatchModeWord.EndsWith, m => m.ContentId.EndsWith(value.First()) },
                        { MatchModeWord.Contains, m => m.ContentId.Contains(value.First()) },
                        { MatchModeWord.NotContains, m => !m.ContentId.Contains(value.First()) },
                        { MatchModeWord.In, m => value.Contains(m.ContentId) },
                        { MatchModeWord.NotIn, m => !value.Contains(m.ContentId) },
                    };
        }

        private static Dictionary<MatchModeWord, Expression<Func<Entities.Version, bool>>> GetOwnerRules(List<string> value)
        {
            return new Dictionary<MatchModeWord, Expression<Func<Entities.Version, bool>>>()
                    {
                        { MatchModeWord.Equals, m => m.OwnerId == value.First() },
                        { MatchModeWord.NotEquals, m => m.OwnerId != value.First() },
                        { MatchModeWord.StartsWith, m => m.OwnerId.StartsWith(value.First()) },
                        { MatchModeWord.EndsWith, m => m.OwnerId.EndsWith(value.First()) },
                        { MatchModeWord.Contains, m => m.OwnerId.Contains(value.First()) },
                        { MatchModeWord.NotContains, m => !m.OwnerId.Contains(value.First()) },
                        { MatchModeWord.In, m => value.Contains(m.OwnerId) },
                        { MatchModeWord.NotIn, m => !value.Contains(m.OwnerId) },
                    };
        }
    }
}
