using SavaDev.Base.Data.Registry.Filter;

namespace SavaDev.Service.Drafts
{
    public class DraftStrictFilterModel : BaseFilter
    {
        public string Entity { get; set; }

        public string Module { get; set; }

        public string OwnerId { get; set; }

        public string? ContentId { get; set; }

        public string? GroupingKey { get; set; }
    }
}
