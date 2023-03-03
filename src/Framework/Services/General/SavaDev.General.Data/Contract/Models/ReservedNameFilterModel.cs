using SavaDev.Base.Data.Registry.Filter;

namespace SavaDev.General.Data.Contract.Models
{
    public class ReservedNameFilterModel : BaseFilter
    {
        public WordFilterField Text { get; set; } = new();
    }
}
