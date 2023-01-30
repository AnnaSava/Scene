using SavaDev.Base.Data.Registry.Filter;

namespace SavaDev.System.Data.Contract.Models
{
    public class ReservedNameFilterModel : BaseFilter
    {
        public WordFilterField Text { get; set; } = new();
    }
}
