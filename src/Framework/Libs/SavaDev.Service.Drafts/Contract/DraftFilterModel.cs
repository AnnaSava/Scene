using SavaDev.Base.Data.Registry.Filter;

namespace SavaDev.Service.Drafts
{
    public class DraftFilterModel : BaseFilter
    {
        public WordFilterField Entity { get; set; }

        public WordFilterField Module { get; set; }

        public WordFilterField Owner { get; set; }

        public WordFilterField ContentId { get; set; }

        public WordFilterField GroupingKey { get; set; }
    }
}
