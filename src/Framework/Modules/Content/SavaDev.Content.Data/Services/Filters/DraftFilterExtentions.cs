using Framework.Base.Types.Enums;
using Savadev.Content.Data.Contract.Models;
using Savadev.Content.Data.Entities;
using SavaDev.Content.Data.Contract.Models;
using System.Linq.Expressions;

namespace Savadev.Content.Data.Services.Filters
{
    public static class DraftFilterExtentions
    {
        public static IQueryable<Draft> ApplyFilters(this IQueryable<Draft> list, DraftFilterModel filter)
        {
            if (filter == null) return list;

            return list;
                //.ApplyModuleFilter(filter)
                //.ApplyEntityFilter(filter)
                //.ApplyOwnerFilter(filter)
                //.ApplyIsDeletedFilter(filter)
                //.ApplyContentIdFilter(filter)
                //.ApplyGroupingKeyFilter(filter);
        }

        //private static IQueryable<Draft> ApplyModuleFilter(this IQueryable<Draft> list, DraftFilterModel filter)
        //{
        //    if (filter.Module?.Value?.Any() ?? false)
        //    {
        //        var rules = GetModuleRules(filter.Module.Value);
        //        list = list.Where(rules[filter.Module.MatchMode]);
        //    }
        //    return list;
        //}

        //private static IQueryable<Draft> ApplyEntityFilter(this IQueryable<Draft> list, DraftFilterModel filter)
        //{
        //    if (filter.Entity?.Value?.Any() ?? false)
        //    {
        //        var rules = GetEntityRules(filter.Entity.Value);
        //        list = list.Where(rules[filter.Entity.MatchMode]);
        //    }
        //    return list;
        //}

        //private static IQueryable<Draft> ApplyContentIdFilter(this IQueryable<Draft> list, DraftFilterModel filter)
        //{
        //    if (filter.ContentId?.Value?.Any() ?? false)
        //    {
        //        var rules = GetContentIdRules(filter.ContentId.Value);
        //        list = list.Where(rules[filter.ContentId.MatchMode]);
        //    }
        //    return list;
        //}

        //private static IQueryable<Draft> ApplyGroupingKeyFilter(this IQueryable<Draft> list, DraftFilterModel filter)
        //{
        //    if (filter.GroupingKey?.Value?.Any() ?? false)
        //    {
        //        var rules = GetGroupingKeyRules(filter.GroupingKey.Value);
        //        list = list.Where(rules[filter.GroupingKey.MatchMode]);
        //    }
        //    return list;
        //}

        //private static IQueryable<Draft> ApplyOwnerFilter(this IQueryable<Draft> list, DraftFilterModel filter)
        //{
        //    if (filter.Owner?.Value?.Any() ?? false)
        //    {
        //        var rules = GetOwnerRules(filter.Owner.Value);
        //        list = list.Where(rules[filter.Owner.MatchMode]);
        //    }
        //    return list;
        //}

        //public static IQueryable<Draft> ApplyIsDeletedFilter(this IQueryable<Draft> list, DraftFilterModel filter)
        //{
        //    list = list.Where(m => m.IsDeleted == filter.IsDeleted);
        //    return list;
        //}

        private static Dictionary<MatchModeWord, Expression<Func<Draft, bool>>> GetModuleRules(List<string> value)
        {
            return new Dictionary<MatchModeWord, Expression<Func<Draft, bool>>>()
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

        private static Dictionary<MatchModeWord, Expression<Func<Draft, bool>>> GetEntityRules(List<string> value)
        {
            return new Dictionary<MatchModeWord, Expression<Func<Draft, bool>>>()
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

        private static Dictionary<MatchModeWord, Expression<Func<Draft, bool>>> GetContentIdRules(List<string> value)
        {
            return new Dictionary<MatchModeWord, Expression<Func<Draft, bool>>>()
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

        private static Dictionary<MatchModeWord, Expression<Func<Draft, bool>>> GetGroupingKeyRules(List<string> value)
        {
            return new Dictionary<MatchModeWord, Expression<Func<Draft, bool>>>()
                    {
                        { MatchModeWord.Equals, m => m.GroupingKey == value.First() },
                        { MatchModeWord.NotEquals, m => m.GroupingKey != value.First() },
                        { MatchModeWord.StartsWith, m => m.GroupingKey.StartsWith(value.First()) },
                        { MatchModeWord.EndsWith, m => m.GroupingKey.EndsWith(value.First()) },
                        { MatchModeWord.Contains, m => m.GroupingKey.Contains(value.First()) },
                        { MatchModeWord.NotContains, m => !m.GroupingKey.Contains(value.First()) },
                        { MatchModeWord.In, m => value.Contains(m.GroupingKey) },
                        { MatchModeWord.NotIn, m => !value.Contains(m.GroupingKey) },
                    };
        }

        private static Dictionary<MatchModeWord, Expression<Func<Draft, bool>>> GetOwnerRules(List<string> value)
        {
            return new Dictionary<MatchModeWord, Expression<Func<Draft, bool>>>()
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


        public static IQueryable<Draft> ApplyStrictFilters(this IQueryable<Draft> list, DraftStrictFilterModel filter)
        {
            if (filter == null) return list;

            return list.ApplyModuleStrictFilter(filter)
                .ApplyEntityStrictFilter(filter)
                .ApplyOwnerIdStrictFilter(filter)
                .ApplyIsDeletedStrictFilter(filter)
                .ApplyContentIdStrictFilter(filter)
                .ApplyGroupingKeyStrictFilter(filter);
        }

        private static IQueryable<Draft> ApplyModuleStrictFilter(this IQueryable<Draft> list, DraftStrictFilterModel filter)
        {
            if (string.IsNullOrEmpty(filter?.Module))
                throw new ArgumentNullException(nameof(DraftStrictFilterModel));

            list = list.Where(m => m.Module == filter.Module);
            return list;
        }

        private static IQueryable<Draft> ApplyEntityStrictFilter(this IQueryable<Draft> list, DraftStrictFilterModel filter)
        {
            if (string.IsNullOrEmpty(filter?.Entity))
                throw new ArgumentNullException(nameof(DraftStrictFilterModel));

            list = list.Where(m => m.Entity == filter.Entity);
            return list;
        }

        private static IQueryable<Draft> ApplyOwnerIdStrictFilter(this IQueryable<Draft> list, DraftStrictFilterModel filter)
        {
            if (!string.IsNullOrEmpty(filter?.OwnerId))
            {
                list = list.Where(m => m.OwnerId == filter.OwnerId);
            }
            return list;
        }

        private static IQueryable<Draft> ApplyIsDeletedStrictFilter(this IQueryable<Draft> list, DraftStrictFilterModel filter)
        {
            //list = list.Where(m => m.IsDeleted == filter.IsDeleted);
            return list;
        }

        private static IQueryable<Draft> ApplyContentIdStrictFilter(this IQueryable<Draft> list, DraftStrictFilterModel filter)
        {
            if (string.IsNullOrEmpty(filter?.ContentId))
                throw new ArgumentNullException(nameof(DraftStrictFilterModel));

            list = filter.ContentId == "0" ? list.Where(m => m.ContentId == null) : list.Where(m => m.ContentId == filter.ContentId);
            return list;
        }

        private static IQueryable<Draft> ApplyGroupingKeyStrictFilter(this IQueryable<Draft> list, DraftStrictFilterModel filter)
        {
            if (!string.IsNullOrEmpty(filter?.GroupingKey))
            {
                list = list.Where(m => m.GroupingKey == filter.GroupingKey);
            }
            return list;
        }
    }
}
