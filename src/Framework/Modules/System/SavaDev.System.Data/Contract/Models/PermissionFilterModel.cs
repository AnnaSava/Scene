using SavaDev.Base.Data.Registry.Filter;

namespace SavaDev.System.Data.Contract.Models
{
    public class PermissionFilterModel : BaseFilter
    {
        public WordFilterField Name { get; set; } = new();
    }
}
